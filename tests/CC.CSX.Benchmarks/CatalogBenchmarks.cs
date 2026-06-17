using System.Buffers;

using BenchmarkDotNet.Attributes;

using CC.CSX;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RenderPlanSpike;

/// <summary>
/// A dynamic-heavy page (product catalog): a loop of cards, each with a conditional class, a computed
/// price, a STRUCTURAL conditional (SALE badge) and a NESTED loop (tags). A more realistic mix than a
/// plain data table — higher dynamic:static ratio. Compares the production paths: htnet render plan,
/// htnet live tree, and Blazor SSR (ToString + the faster WriteHtmlTo).
/// </summary>
[MemoryDiagnoser]
public class CatalogBenchmarks
{
    [Params(10, 100, 1000)]
    public int Products { get; set; }

    internal static Product[] Data = [];
    readonly DiscardBufferWriter sink = new();

    ServiceProvider services = null!;
    HtmlRenderer renderer = null!;

    [GlobalSetup]
    public void Setup()
    {
        RenderOptions.Indent = 0;
        Data = System.Linq.Enumerable.Range(0, Products)
            .Select(i => new Product($"Product {i}", 1.5m * i, (i & 1) == 0, i % 3 == 0,
                i % 4 == 0 ? new[] { "new", "hot" } : new[] { "sale" }))
            .ToArray();

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
    public long Htnet_RenderPlan()
    {
        sink.Reset();
        CatalogViews__Optimized.Catalog(Data).WriteTo(sink);
        return sink.Count;
    }

    [Benchmark(Baseline = true)]
    public long Htnet_Live()
    {
        sink.Reset();
        CatalogViews.Catalog(Data).WriteTo(sink);
        return sink.Count;
    }

    [Benchmark]
    public string Blazor_ToString()
        => renderer.Dispatcher.InvokeAsync(async () =>
            (await renderer.RenderComponentAsync<CatalogComponent>()).ToHtmlString()).GetAwaiter().GetResult();

    [Benchmark]
    public void Blazor_WriteTo()
        => renderer.Dispatcher.InvokeAsync(async () =>
        {
            var root = await renderer.RenderComponentAsync<CatalogComponent>();
            root.WriteHtmlTo(System.IO.TextWriter.Null);
        }).GetAwaiter().GetResult();
}

/// <summary>The same catalog page as a Blazor component (hand-lowered .razor equivalent).</summary>
public class CatalogComponent : ComponentBase
{
    protected override void BuildRenderTree(RenderTreeBuilder b)
    {
        b.OpenElement(0, "div"); b.AddAttribute(1, "class", "catalog");
        b.OpenElement(2, "h1"); b.AddContent(3, "Products"); b.CloseElement();
        b.OpenElement(4, "div"); b.AddAttribute(5, "class", "grid");
        foreach (var p in CatalogBenchmarks.Data)
        {
            b.OpenElement(6, "div");
            b.AddAttribute(7, "class", p.InStock ? "card in-stock" : "card out-of-stock");
            b.OpenElement(8, "h3"); b.AddContent(9, p.Name); b.CloseElement();
            b.OpenElement(10, "p"); b.AddAttribute(11, "class", "price"); b.AddContent(12, $"${p.Price}"); b.CloseElement();
            if (p.OnSale)
            {
                b.OpenElement(13, "span"); b.AddAttribute(14, "class", "badge"); b.AddContent(15, "SALE"); b.CloseElement();
            }
            b.OpenElement(16, "ul"); b.AddAttribute(17, "class", "tags");
            foreach (var t in p.Tags)
            {
                b.OpenElement(18, "li"); b.AddContent(19, t); b.CloseElement();
            }
            b.CloseElement(); // ul
            b.CloseElement(); // card div
        }
        b.CloseElement(); // grid
        b.CloseElement(); // catalog
    }
}
