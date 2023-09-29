using Web;
using Microsoft.AspNetCore.Mvc;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;
using static Web.Templates;
using static CC.CSX.Htmx.HtmxAttributes;
using static CC.CSX.Web.Extensions;
var todos = TodoGenerator.GenerateTodos().ToArray();
var builder = WebApplication.CreateSlimBuilder(args);
var app = builder.Build();


//Index: Page
app.MapGet("", () => Render(Html(
    Head(
        Title("CSX Sample"),
        Meta(charset("utf-8")),
        HtmxScriptImports),
    Body(
        Div(@class("container"),
            H1(@class("text-center"), "CSX Sample"),
            A(href("/todos"), "Todos")))
)));

app.MapGet("todos", () => Render(TodosPage(todos)));

app.MapPost("todos", ([AsParameters] TodoRequest form) =>
{
    var todo = new Todo(todos.Max(t => t.Id) + 1, form.Title, form.DueBy, form.IsCompleteBool);
    todos = [todo, ..todos];
    return Render(TodosPage(todos));
}).DisableAntiforgery();

app.Run();
class TodoRequest
{
    [FromForm] public required string Title { get; set; }
    [FromForm] public DateTime? DueBy { get; set; } = default;
    [FromForm] public string? IsComplete { get; set; } = "off";
    public bool IsCompleteBool => IsComplete == "on";
}