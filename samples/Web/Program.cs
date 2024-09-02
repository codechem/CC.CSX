var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


static HtmlNode Master(string title, params HtmlNode[] content) => Html(
    Meta(charset("utf-8")),
    Head(
        Title("Htnet Demo"),
        Meta(charset("utf-8")),
        HtmxImports),
    Body(
        H1(@class("text-center"), title),
        Ul(
            Li(AHref("/", "Home")), 
            Li(AHref("/counter", "Counter"))
        ),
        Hr(),
        content,
        Hr()
    )
);

app.MapGet("", () => Render(Master("Home")));

int counter = 0;

app.MapGet("counter", () => Render(
    Master("Counter",
        Button("-", hxPost("/decrement", target: "#counter")),
        B(Label(id("counter"), counter.ToString())),
        Button("+", hxPost("/increment", target: "#counter"), hxPushUrl("false"))
    )
));
app.MapPost("increment/", () => Render($"{++counter}"));
app.MapPost("decrement/", () => Render($"{--counter}"));
app.Run();