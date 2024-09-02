using CC.CSX;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;
using static CC.CSX.Htmx.HtmxAttributes;
using static System.Text.Json.JsonSerializer;

public static class Views
{
    public static HtmlNode MainPage = HtmxPage(Div(
        H1("Main page"),
        Button("Bored", "Bla", "ta", 
            hxGet("/apis/bored/"),
            hxTarget("#results")),
        Button("Tell me a joke",
            hxGet("/apis/joke"),
            hxTarget("#results")),
        Div(id("results"))
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
            : A(href(bored.link), "Link" )));

    public static HtmlNode HtmxPage(HtmlItem root) => Html(
        Head(HtmxImports),
        Body(root));
}