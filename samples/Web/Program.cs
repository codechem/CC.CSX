using Web;
using CC.CSX;
using static CC.CSX.HtmlElements;

var builder = WebApplication.CreateSlimBuilder(args);
var app = builder.Build();
var samples = TodoGenerator.GenerateTodos().ToArray();
app.MapGet("/todos", () =>
  Html(
    Body(
      H1("Sample todos"),
      Ul([..samples.Select(todo =>
         Li(
           Label($"Title:{todo.Title}"),
           " | ",
           Label($"Date: {todo.DueBy}")))]))
  ).ToResponse());

app.MapGet("/todos2", () =>
{
    var templ = (HtmlNode[] items) => Html(Body(
      H1("Sample todos"), Ul(items)));

    var items = samples.Select(todo => Li(
      Label($"Title:{todo.Title}"),
      Label($"Date: {todo.DueBy}"))).ToArray();

    return templ(items).ToResponse();
});
app.Run();