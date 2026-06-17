using System.Buffers;
using System.Text;

using BenchmarkDotNet.Attributes;

using CC.CSX;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

/// <summary>
/// A realistic data-table page rendered four ways, all emitting the same HTML to a discarding sink
/// at Indent 0:
///  - HandWritten     : direct byte writing (the floor)
///  - Htnet_RenderPlan : the generated [RenderOptimized] builder (Views__Optimized.Report)
///  - Htnet_Live       : building the CC.CSX HtmlNode tree, then WriteTo (current default path)
///  - Blazor_SSR       : the same page as a Blazor component via HtmlRenderer (what .razor compiles to)
/// </summary>
[MemoryDiagnoser]
public class RealisticBenchmarks
{
    [Params(10, 100, 1000)]
    public int Rows { get; set; }

    internal static (int id, string name, string email)[] Data = [];
    readonly DiscardBufferWriter sink = new();

    ServiceProvider services = null!;
    HtmlRenderer renderer = null!;

    static readonly byte[] Prefix = U8("<div class=\"uk-container\"><h1>Report</h1><table class=\"uk-table\"><thead><tr><th>Id</th><th>Name</th><th>Email</th></tr></thead><tbody>");
    static readonly byte[] RowOpen = U8("<tr class=\"");
    static readonly byte[] AfterClass = U8("\"><td>");
    static readonly byte[] Sep = U8("</td><td>");
    static readonly byte[] RowEnd = U8("</td></tr>");
    static readonly byte[] Suffix = U8("</tbody></table></div>");
    static byte[] U8(string s) => Encoding.UTF8.GetBytes(s);

    [GlobalSetup]
    public void Setup()
    {
        RenderOptions.Indent = 0;
        Data = System.Linq.Enumerable.Range(0, Rows)
            .Select(i => (i, $"name-{i}", $"user{i}@example.com")).ToArray();

        var sc = new ServiceCollection();
        sc.AddLogging();
        services = sc.BuildServiceProvider();
        renderer = new HtmlRenderer(services, services.GetRequiredService<ILoggerFactory>());
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        renderer.Dispose();
        services.Dispose();
    }

    [Benchmark]
    public long HandWritten()
    {
        sink.Reset();
        using var w = new Utf8HtmlWriter(sink);
        w.WriteRawUtf8(Prefix);
        Span<char> num = stackalloc char[12];
        foreach (var r in Data)
        {
            w.WriteRawUtf8(RowOpen);
            w.Write((r.id & 1) == 0 ? "even" : "odd");
            w.WriteRawUtf8(AfterClass);
            r.id.TryFormat(num, out int n);
            w.Write(num[..n]);
            w.WriteRawUtf8(Sep);
            w.Write(r.name);
            w.WriteRawUtf8(Sep);
            w.Write(r.email);
            w.WriteRawUtf8(RowEnd);
        }
        w.WriteRawUtf8(Suffix);
        w.Flush();
        return sink.Count;
    }

    [Benchmark]
    public long Htnet_RenderPlan()
    {
        sink.Reset();
        RenderPlanSpike.Views__Optimized.Report(Data).WriteTo(sink);
        return sink.Count;
    }

    [Benchmark(Baseline = true)]
    public long Htnet_Live()
    {
        sink.Reset();
        RenderPlanSpike.Views.Report(Data).WriteTo(sink);
        return sink.Count;
    }

    // typical Blazor SSR: builds the full HTML string
    [Benchmark]
    public string Blazor_ToString()
        => renderer.Dispatcher.InvokeAsync(async () =>
            (await renderer.RenderComponentAsync<ReportComponent>()).ToHtmlString()).GetAwaiter().GetResult();

    // Blazor optimization 1: render straight to a TextWriter (no output string materialized)
    [Benchmark]
    public void Blazor_WriteTo()
        => renderer.Dispatcher.InvokeAsync(async () =>
        {
            var root = await renderer.RenderComponentAsync<ReportComponent>();
            root.WriteHtmlTo(System.IO.TextWriter.Null);
        }).GetAwaiter().GetResult();

    // Blazor optimization 2: static HTML as raw markup (AddMarkupContent) — fewer/cheaper frames,
    // the hand-tuned analog of baked static chunks — also written to a TextWriter
    [Benchmark]
    public void Blazor_Markup_WriteTo()
        => renderer.Dispatcher.InvokeAsync(async () =>
        {
            var root = await renderer.RenderComponentAsync<ReportComponentMarkup>();
            root.WriteHtmlTo(System.IO.TextWriter.Null);
        }).GetAwaiter().GetResult();
}

/// <summary>The same data-table page as a Blazor component (hand-lowered .razor equivalent).</summary>
public class ReportComponent : ComponentBase
{
    protected override void BuildRenderTree(RenderTreeBuilder b)
    {
        b.OpenElement(0, "div");
        b.AddAttribute(1, "class", "uk-container");
        b.OpenElement(2, "h1");
        b.AddContent(3, "Report");
        b.CloseElement();
        b.OpenElement(4, "table");
        b.AddAttribute(5, "class", "uk-table");
        b.OpenElement(6, "thead");
        b.OpenElement(7, "tr");
        b.OpenElement(8, "th"); b.AddContent(9, "Id"); b.CloseElement();
        b.OpenElement(10, "th"); b.AddContent(11, "Name"); b.CloseElement();
        b.OpenElement(12, "th"); b.AddContent(13, "Email"); b.CloseElement();
        b.CloseElement(); // tr
        b.CloseElement(); // thead
        b.OpenElement(14, "tbody");
        foreach (var r in RealisticBenchmarks.Data)
        {
            b.OpenElement(15, "tr");
            b.AddAttribute(16, "class", (r.id & 1) == 0 ? "even" : "odd");
            b.OpenElement(17, "td"); b.AddContent(18, r.id); b.CloseElement();
            b.OpenElement(19, "td"); b.AddContent(20, r.name); b.CloseElement();
            b.OpenElement(21, "td"); b.AddContent(22, r.email); b.CloseElement();
            b.CloseElement(); // tr
        }
        b.CloseElement(); // tbody
        b.CloseElement(); // table
        b.CloseElement(); // div
    }
}

/// <summary>
/// The same page, hand-optimized the way a Blazor dev would for SSR throughput: static HTML emitted
/// as raw markup via <see cref="RenderTreeBuilder.AddMarkupContent"/> (one cheap frame, no escaping),
/// only the dynamic values via AddContent — the Blazor analog of baked static chunks.
/// </summary>
public class ReportComponentMarkup : ComponentBase
{
    protected override void BuildRenderTree(RenderTreeBuilder b)
    {
        b.AddMarkupContent(0, "<div class=\"uk-container\"><h1>Report</h1><table class=\"uk-table\"><thead><tr><th>Id</th><th>Name</th><th>Email</th></tr></thead><tbody>");
        foreach (var r in RealisticBenchmarks.Data)
        {
            b.AddMarkupContent(1, "<tr class=\"");
            b.AddContent(2, (r.id & 1) == 0 ? "even" : "odd");
            b.AddMarkupContent(3, "\"><td>");
            b.AddContent(4, r.id);
            b.AddMarkupContent(5, "</td><td>");
            b.AddContent(6, r.name);
            b.AddMarkupContent(7, "</td><td>");
            b.AddContent(8, r.email);
            b.AddMarkupContent(9, "</td></tr>");
        }
        b.AddMarkupContent(10, "</tbody></table></div>");
    }
}
