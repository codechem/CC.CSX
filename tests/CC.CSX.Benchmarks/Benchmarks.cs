using System.Buffers;
using System.Text;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using CC.CSX;
using CC.CSX.Css;
using CC.CSX.Css.Tailwind;

using static CC.CSX.HtmlAttributes;
using static CC.CSX.HtmlElements;

// run everything: dotnet run -c Release --project tests/CC.CSX.Benchmarks -- --filter *
// quick pass:     ... -- --filter * --job short
BenchmarkSwitcher
    .FromTypes([typeof(RenderBenchmarks), typeof(ScalingBenchmarks), typeof(RequestBenchmarks), typeof(RenderPlanBenchmarks), typeof(RealisticBenchmarks), typeof(CatalogBenchmarks), typeof(CssCompositionBenchmarks), typeof(BlazorComparisonBenchmarks)])
    .Run(args.Length > 0 ? args : ["--filter", "*"]);

/// <summary>
/// Render paths for a fixed mid-sized page, against a hand-written StringBuilder baseline
/// (the theoretical floor). Indent=0 is the production-shaped output; the baseline always
/// writes compact HTML, so compare ratios on Indent=0.
/// </summary>
[MemoryDiagnoser]
public class RenderBenchmarks
{
    readonly HtmlNode node = Templates.MainPage(null,
        Div(
            Form(@class("uk-form"), method("post"), action("/test"),
                Input(type("text"), name("name"), value("test")),
                Input(type("text"), name("age"), value("10")),
                Button(type("submit"), "Submit")
            )
        ));

    [Params(0, 2)]
    public int Indent { get; set; }

    [GlobalSetup]
    public void Setup() => RenderOptions.Indent = Indent;

    [Benchmark(Baseline = true)]
    public string HandWritten_StringBuilder()
    {
        var sb = new StringBuilder();
        sb.Append("<html><head><title>Hello, World!</title>");
        sb.Append("<link href=\"https://cdn.jsdelivr.net/npm/uikit@3.16.23/dist/css/uikit.min.css\" rel=\"stylesheet\">");
        sb.Append("<script src=\"https://cdn.jsdelivr.net/npm/uikit@3.16.23/dist/js/uikit.min.js\"></script>");
        sb.Append("<script src=\"https://cdn.jsdelivr.net/npm/uikit@3.16.23/dist/js/uikit-icons.min.js\"></script>");
        sb.Append("<script src=\"https://unpkg.com/nunjucks@3.2.3/browser/nunjucks.js\"></script>");
        sb.Append("<script src=\"https://unpkg.com/htmx.org@1.9.3\"></script>");
        sb.Append("<script src=\"https://unpkg.com/hyperscript.org@0.9.7\"></script>");
        sb.Append("<script>").Append(Templates.JsonEncodeScript).Append("</script>");
        sb.Append("<script>").Append(Templates.NunjucksTemplateScript).Append("</script>");
        sb.Append("</head><body><div class=\"uk-container\" hx-ext=\"client-side-templates\">");
        sb.Append("<div><form class=\"uk-form\" method=\"post\" action=\"/test\">");
        sb.Append("<input type=\"text\" name=\"name\" value=\"test\"></input>");
        sb.Append("<input type=\"text\" name=\"age\" value=\"10\"></input>");
        sb.Append("<button type=\"submit\">Submit</button>");
        sb.Append("</form></div></div></body></html>");
        return sb.ToString();
    }

    [Benchmark]
    public string Render_ToString() => node.ToString();

    [Benchmark]
    public string Render_StringBuilder()
    {
        var sb = new StringBuilder();
        node.AppendTo(ref sb);
        return sb.ToString();
    }

    [Benchmark]
    public string Render_TextWriter()
    {
        var tw = new StringWriter() as TextWriter;
        node.WriteTo(ref tw);
        return tw.ToString()!;
    }

    [Benchmark]
    public HtmlNode Build_Tree() =>
        Templates.MainPage(null,
            Div(
                Form(@class("uk-form"), method("post"), action("/test"),
                    Input(type("text"), name("name"), value("test")),
                    Input(type("text"), name("age"), value("10")),
                    Button(type("submit"), "Submit")
                )
            ));
}

/// <summary>
/// How construction and rendering scale with document size (a table with N rows).
/// </summary>
[MemoryDiagnoser]
public class ScalingBenchmarks
{
    HtmlNode table = CC.CSX.Fragment.Empty;

    [Params(10, 100, 1000)]
    public int Rows { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        RenderOptions.Indent = 0;
        table = BuildTable();
    }

    readonly DiscardBufferWriter sink = new();

    [Benchmark]
    public HtmlNode Build_Table() => BuildTable();

    [Benchmark(Baseline = true)]
    public string Render_Table()
    {
        var tw = new StringWriter() as TextWriter;
        table.WriteTo(ref tw);
        return tw.ToString()!;
    }

    // The production-shaped path: stream UTF-8 into an IBufferWriter<byte> (like a response PipeWriter),
    // so the output is never held as one growing string/LOH buffer.
    [Benchmark]
    public long Render_Table_Utf8()
    {
        sink.Reset();
        table.WriteTo(sink);
        return sink.Count;
    }

    HtmlNode BuildTable() =>
        Table(@class("uk-table"),
            Thead(Tr(Th("Id"), Th("Name"), Th("Email"))),
            Tbody(Enumerable.Range(0, Rows)
                .Select(i => Tr(@class(i % 2 == 0 ? "even" : "odd"),
                    Td(i),
                    Td($"name-{i}"),
                    Td($"user{i}@example.com")))
                .ToArray()));
}

/// <summary>
/// The full per-request cycle a server pays: build the view tree, then stream it as UTF-8 into
/// the response (modelled by an <see cref="IBufferWriter{T}"/>). Allocated-per-op is the per-request
/// GC pressure that sets the throughput ceiling; from it, RPS_ceiling ≈ (GC-budget bytes/s) / Allocated.
/// </summary>
[MemoryDiagnoser]
public class RequestBenchmarks
{
    [Params(10, 100, 1000)]
    public int Rows { get; set; }

    readonly DiscardBufferWriter sink = new();

    // Static chrome, cached once (Release default). Built lazily on first render at Indent=0.
    static readonly HtmlNode CachedHead = SiteHead().Cache();
    static readonly HtmlNode CachedNav = SiteNav().Cache();
    static readonly HtmlNode CachedFooter = SiteFooter().Cache();

    [GlobalSetup]
    public void Setup()
    {
        RenderOptions.Indent = 0;
        FragmentCache.Enabled = true; // benchmark the caching path explicitly
    }

    [Benchmark(Baseline = true)]
    public long BuildAndRender()
    {
        HtmlNode page = Html(SiteHead(), Body(SiteNav(), Content(Rows), SiteFooter()));
        sink.Reset();
        page.WriteTo(sink);
        return sink.Count;
    }

    [Benchmark]
    public long BuildAndRender_CachedChrome()
    {
        HtmlNode page = Html(CachedHead, Body(CachedNav, Content(Rows), CachedFooter));
        sink.Reset();
        page.WriteTo(sink);
        return sink.Count;
    }

    // --- representative page parts ---

    static HtmlNode SiteHead() =>
        Head(
            Meta(charset("utf-8")),
            Meta(name("viewport"), content("width=device-width, initial-scale=1")),
            Title("Report"),
            Link(rel("stylesheet"), href("/app.css")),
            Link(rel("preconnect"), href("https://fonts.example.com")),
            Script(src("/lib/htmx.min.js")),
            Script(src("/lib/app.js")));

    static HtmlNode SiteNav() =>
        Div(@class("navbar"),
            A(@class("brand"), href("/"), "Acme"),
            A(href("/products"), "Products"),
            A(href("/pricing"), "Pricing"),
            A(href("/docs"), "Docs"),
            A(href("/blog"), "Blog"),
            A(href("/about"), "About"),
            A(href("/login"), "Sign in"));

    static HtmlNode SiteFooter() =>
        Div(@class("footer"),
            A(href("/terms"), "Terms"),
            A(href("/privacy"), "Privacy"),
            A(href("/contact"), "Contact"),
            P("(c) 2026 Acme, Inc."));

    static HtmlNode Content(int rows) =>
        Div(@class("uk-container"),
            H1("Report"),
            Table(@class("uk-table"),
                Thead(Tr(Th("Id"), Th("Name"), Th("Email"))),
                Tbody(Enumerable.Range(0, rows)
                    .Select(i => Tr(@class(i % 2 == 0 ? "even" : "odd"),
                        Td(i),
                        Td($"name-{i}"),
                        Td($"user{i}@example.com")))
                    .ToArray())));
}

/// <summary>
/// Static/dynamic render plans for the 1000-row table: full live build+render vs a coarse plan
/// (chrome baked, rows rebuilt live) vs a hand-written fine plan (row scaffold baked, only cell
/// values written) — the ceiling a [RenderOptimized] generator would target. All write the same
/// bytes to a discarding sink at Indent 0.
/// </summary>
[MemoryDiagnoser]
public class RenderPlanBenchmarks
{
    [Params(10, 100, 1000)]
    public int Rows { get; set; }

    (int id, string name, string email)[] rows = [];
    readonly DiscardBufferWriter sink = new();
    RenderPlan coarse = null!;

    // baked static fragments for the fine plan
    static readonly byte[] FinePrefix = U8("<table class=\"uk-table\"><thead><tr><th>Id</th><th>Name</th><th>Email</th></tr></thead><tbody>");
    static readonly byte[] RowClassOpen = U8("<tr class=\"");
    static readonly byte[] RowAfterClass = U8("\"><td>");
    static readonly byte[] CellSep = U8("</td><td>");
    static readonly byte[] RowEnd = U8("</td></tr>");
    static readonly byte[] FineSuffix = U8("</tbody></table>");
    static byte[] U8(string s) => Encoding.UTF8.GetBytes(s);

    [GlobalSetup]
    public void Setup()
    {
        RenderOptions.Indent = 0;
        rows = Enumerable.Range(0, Rows)
            .Select(i => (i, $"name-{i}", $"user{i}@example.com")).ToArray();
        coarse = RenderPlan.Compile(BuildTable());
    }

    // current per-request cost: build the whole tree then stream it
    [Benchmark(Baseline = true)]
    public long Live_BuildAndRender()
    {
        sink.Reset();
        BuildTable().WriteTo(sink);
        return sink.Count;
    }

    // chrome baked once; rows still rebuilt live each execution
    [Benchmark]
    public long Plan_Coarse()
    {
        sink.Reset();
        coarse.WriteTo(sink);
        return sink.Count;
    }

    // the generator's target: no HtmlNode tree built at all, only cell values written
    [Benchmark]
    public long Plan_Fine_HandWritten()
    {
        sink.Reset();
        using var w = new Utf8HtmlWriter(sink);
        w.WriteRawUtf8(FinePrefix);
        Span<char> num = stackalloc char[12];
        foreach (var r in rows)
        {
            w.WriteRawUtf8(RowClassOpen);
            w.Write((r.id & 1) == 0 ? "even" : "odd");
            w.WriteRawUtf8(RowAfterClass);
            r.id.TryFormat(num, out int n);
            w.Write(num[..n]);
            w.WriteRawUtf8(CellSep);
            w.Write(r.name);
            w.WriteRawUtf8(CellSep);
            w.Write(r.email);
            w.WriteRawUtf8(RowEnd);
        }
        w.WriteRawUtf8(FineSuffix);
        w.Flush();
        return sink.Count;
    }

    HtmlNode BuildTable() =>
        Table(@class("uk-table"),
            Thead(Tr(Th("Id"), Th("Name"), Th("Email"))),
            Tbody(Each(rows, r => Tr(@class((r.id & 1) == 0 ? "even" : "odd"),
                Td(r.id),
                Td(r.name),
                Td(r.email)))));
}

/// <summary>
/// An <see cref="IBufferWriter{T}"/> that discards output into one reused pooled-ish scratch
/// buffer (just counting bytes). Models streaming to a response without measuring the cost of
/// storing the output — so render allocations reflect the renderer itself, not the sink.
/// </summary>
public sealed class DiscardBufferWriter : IBufferWriter<byte>
{
    byte[] _scratch = new byte[64 * 1024];
    public long Count { get; private set; }
    public void Reset() => Count = 0;
    public void Advance(int count) => Count += count;
    public Memory<byte> GetMemory(int sizeHint = 0) => Ensure(sizeHint);
    public Span<byte> GetSpan(int sizeHint = 0) => Ensure(sizeHint);
    byte[] Ensure(int sizeHint)
    {
        if (sizeHint > _scratch.Length) _scratch = new byte[sizeHint];
        return _scratch;
    }
}

/// <summary>
/// Cost of composing CssClass / Tailwind utilities per call versus reusing a
/// prebuilt static readonly virtual class (the recommended hot-path pattern).
/// </summary>
[MemoryDiagnoser]
public class CssCompositionBenchmarks
{
    static readonly CssClass PrebuiltCard = Tw.bgWhite + Tw.roundedLg + Tw.shadowMd + Tw.p4;

    [Benchmark(Baseline = true)]
    public CssClass Prebuilt_StaticReadonly() => PrebuiltCard;

    [Benchmark]
    public CssClass Compose_PlusOperator() => Tw.bgWhite + Tw.roundedLg + Tw.shadowMd + Tw.p4;

    [Benchmark]
    public CssClass Compose_Method() => CssClass.Compose(Tw.bgWhite, Tw.roundedLg, Tw.shadowMd, Tw.p4);

    [Benchmark]
    public CssClass Variant_Nested() => Tw.md(Tw.hover(Tw.bgSky600));

    [Benchmark]
    public string ClassAttribute_FromComposed() => @class(PrebuiltCard, "extra").ToString();
}
