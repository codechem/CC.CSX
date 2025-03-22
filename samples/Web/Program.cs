WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

int counter = 0;
app.MapGet("/", () => Render(
    Master("Counter",
        Div(
           Button("-", hxPost("/decrement", target: "#counter")),
           B(Label(id("counter"), counter)),
           Button("+", hxPost("/increment", target: "#counter")),
           "|",
           Button("Reset", hxPost("/reset", target: "#counter"))
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
            HtmxImports
        ),
        Body(
            H1(@class("text-center"), title),
            content,
            Hr()
        )
    );
}