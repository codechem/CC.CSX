using CC.CSX;
using CC.CSX.Css;

using static System.Text.Json.JsonSerializer;
using static CC.CSX.HtmlAttributes;
using static CC.CSX.HtmlElements;
using static CC.CSX.Htmx.HtmxAttributes;

public static class Views
{
    // strongly typed classes, hand-authored (see samples/Web for the .css source-generator variant)
    static readonly CssClass Action = new("action");
    static readonly CssClass Results = new("results");

    static readonly CssBundle Styles = new("htmx-sample", """
        body { font-family: sans-serif; max-width: 40rem; margin: 2rem auto; }
        .action { padding: .5rem 1rem; margin-right: .5rem; cursor: pointer; }
        .results { margin-top: 1.5rem; padding: 1rem; border: 1px solid #ddd; border-radius: 8px; }
        """);

    public static HtmlNode MainPage = HtmxPage(Div(
        H1("Main page"),
        Button(@class(Action), "Bored",
            hxGet("/apis/bored/"),
            hxTarget("#results")),
        Button(@class(Action), "Tell me a joke",
            hxGet("/apis/joke"),
            hxTarget("#results")),
        Div(id("results"), @class(Results))
    ));

    public static HtmlNode JokeView(dynamic joke) => Div(
        P("Kind: " + (string)(joke.type)),
        P(joke.setup),
        H4("Punchline"), P(joke.punchline),
        P("Code:", Code(Serialize(joke))));

    public static HtmlNode BoredItem(dynamic bored) => Div(
        P($"Activity: {bored.activity}"),
        P($"Type: {bored.type}"),
        P(string.IsNullOrEmpty(bored.link)
            ? None
            : A(href(bored.link), "Link")));

    public static HtmlNode HtmxPage(HtmlItem root) => Html(
        Head(HtmxImports, CssImports.Inline(Styles)),
        Body(root));
}
