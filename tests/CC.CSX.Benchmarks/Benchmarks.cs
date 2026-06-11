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
    .FromTypes([typeof(RenderBenchmarks), typeof(ScalingBenchmarks), typeof(CssCompositionBenchmarks), typeof(BlazorComparisonBenchmarks)])
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

    [Benchmark]
    public HtmlNode Build_Table() => BuildTable();

    [Benchmark]
    public string Render_Table()
    {
        var tw = new StringWriter() as TextWriter;
        table.WriteTo(ref tw);
        return tw.ToString()!;
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
