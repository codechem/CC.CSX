WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

int counter = 0;
// a virtual (higher-order) class: expands to "btn btn--accent"
CssClass accentButton = Site.btn + Site.btnAccent;
app.MapGet("/", () => Render(
    Master("Counter",
        Div(@class(Site.panel),
           Button("-", @class(accentButton), hxPost("/decrement", target: "#counter")),
           B(Label(id("counter"), @class(Site.counter), counter)),
           Button("+", @class(accentButton), hxPost("/increment", target: "#counter")),
           "|",
           Button("Reset", @class(Site.btn), hxPost("/reset", target: "#counter"))
       )
    )
));

app.MapPost("increment", () => Render($"{++counter}"));
app.MapPost("decrement", () => Render($"{--counter}"));
app.MapPost("reset", () => Render($"{counter = 0}"));
app.Run();
static HtmlNode Master(string title, params HtmlNode[] content)
{
    return Html(
        Meta(charset("utf-8")),
        Head(
            Title(title),
            Meta(charset("utf-8")),
            HtmxImports,
            CssImports.Inline(Site.Bundle)
        ),
        Body(
            style(fontFamily("sans-serif"), CSS.color("#333")),
            "how are you",
            H2("this is great"),
            H1(@class(Site.textCenter), title),
            content,
            Hr()
        )
    );
}