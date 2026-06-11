using BenchmarkDotNet.Attributes;

using CC.CSX;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using static CC.CSX.HtmlAttributes;
using static CC.CSX.HtmlElements;

/// <summary>
/// Renders the same page through htnet and through Blazor's SSR <see cref="HtmlRenderer"/>.
/// The Blazor side is a <see cref="ComponentBase"/> using <see cref="RenderTreeBuilder"/>
/// directly — exactly what a .razor file compiles to, so the comparison is fair.
/// </summary>
[MemoryDiagnoser]
public class BlazorComparisonBenchmarks
{
    readonly HtmlNode node = Templates.MainPage(null,
        Div(
            Form(@class("uk-form"), method("post"), action("/test"),
                Input(type("text"), name("name"), value("test")),
                Input(type("text"), name("age"), value("10")),
                Button(type("submit"), "Submit")
            )
        ));

    ServiceProvider services = null!;
    HtmlRenderer renderer = null!;

    [GlobalSetup]
    public void Setup()
    {
        RenderOptions.Indent = 0;
        var collection = new ServiceCollection();
        collection.AddLogging();
        services = collection.BuildServiceProvider();
        renderer = new HtmlRenderer(services, services.GetRequiredService<ILoggerFactory>());
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        renderer.Dispose();
        services.Dispose();
    }

    [Benchmark(Baseline = true)]
    public string Htnet_TextWriter()
    {
        var tw = new StringWriter() as TextWriter;
        node.WriteTo(ref tw);
        return tw.ToString()!;
    }

    [Benchmark]
    public Task<string> Blazor_HtmlRenderer() =>
        renderer.Dispatcher.InvokeAsync(async () =>
        {
            var output = await renderer.RenderComponentAsync<MainPageComponent>();
            return output.ToHtmlString();
        });
}

/// <summary>The benchmark page as a Blazor component (hand-lowered .razor equivalent).</summary>
public class MainPageComponent : ComponentBase
{
    static void Script(RenderTreeBuilder b, int seq, string src)
    {
        b.OpenElement(seq, "script");
        b.AddAttribute(seq + 1, "src", src);
        b.CloseElement();
    }

    static void InlineScript(RenderTreeBuilder b, int seq, string body)
    {
        b.OpenElement(seq, "script");
        b.AddContent(seq + 1, new MarkupString(body));
        b.CloseElement();
    }

    static void Input(RenderTreeBuilder b, int seq, string name, string value)
    {
        b.OpenElement(seq, "input");
        b.AddAttribute(seq + 1, "type", "text");
        b.AddAttribute(seq + 2, "name", name);
        b.AddAttribute(seq + 3, "value", value);
        b.CloseElement();
    }

    protected override void BuildRenderTree(RenderTreeBuilder b)
    {
        b.OpenElement(0, "html");
        b.OpenElement(1, "head");
        b.OpenElement(2, "title");
        b.AddContent(3, "Hello, World!");
        b.CloseElement();
        b.OpenElement(4, "link");
        b.AddAttribute(5, "href", "https://cdn.jsdelivr.net/npm/uikit@3.16.23/dist/css/uikit.min.css");
        b.AddAttribute(6, "rel", "stylesheet");
        b.CloseElement();
        Script(b, 7, "https://cdn.jsdelivr.net/npm/uikit@3.16.23/dist/js/uikit.min.js");
        Script(b, 9, "https://cdn.jsdelivr.net/npm/uikit@3.16.23/dist/js/uikit-icons.min.js");
        Script(b, 11, "https://unpkg.com/nunjucks@3.2.3/browser/nunjucks.js");
        Script(b, 13, "https://unpkg.com/htmx.org@1.9.3");
        Script(b, 15, "https://unpkg.com/hyperscript.org@0.9.7");
        InlineScript(b, 17, Templates.JsonEncodeScript);
        InlineScript(b, 19, Templates.NunjucksTemplateScript);
        b.CloseElement(); // head
        b.OpenElement(21, "body");
        b.OpenElement(22, "div");
        b.AddAttribute(23, "class", "uk-container");
        b.AddAttribute(24, "hx-ext", "client-side-templates");
        b.OpenElement(25, "div");
        b.OpenElement(26, "form");
        b.AddAttribute(27, "class", "uk-form");
        b.AddAttribute(28, "method", "post");
        b.AddAttribute(29, "action", "/test");
        Input(b, 30, "name", "test");
        Input(b, 35, "age", "10");
        b.OpenElement(40, "button");
        b.AddAttribute(41, "type", "submit");
        b.AddContent(42, "Submit");
        b.CloseElement(); // button
        b.CloseElement(); // form
        b.CloseElement(); // div
        b.CloseElement(); // div
        b.CloseElement(); // body
        b.CloseElement(); // html
    }
}
