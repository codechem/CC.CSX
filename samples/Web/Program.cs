var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

int counter = 0;

app.MapGet("/", () => Render(
    Master("Counter",
        Button("-", hxPost("/decrement", target: "#counter")),
        B(Label(id("counter"), counter)),
        Button("+", hxPost("/increment", target: "#counter"))
    )
));
app.MapPost("increment/", () => Render($"{++counter}"));
app.MapPost("decrement/", () => Render($"{--counter}"));
app.Run();


static HtmlNode Master(string title, params HtmlNode[] content) => Html(
    Meta(charset("utf-8")),
    Head(
        Title(title),
        Meta(charset("utf-8")),
        HtmxImports
        ),
        
    Body(
        H1(@class("text-center", "adasd", "asdfqer"), title, Label("Costa"), "adasd", style("color: red;")),
        content,
        Hr()
    )
);