var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Render(Html(
    Head(
        Title("Hello CSX!")),
    Body(
        H1("Hello CSX!"))
)));

app.Run();
