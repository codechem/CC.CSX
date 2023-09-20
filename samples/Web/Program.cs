using Web;
using Microsoft.AspNetCore.Mvc;
using CC.CSX.Web;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;

using static Web.Templates;
var samples = TodoGenerator.GenerateTodos().ToArray();
var app = WebApplication.CreateSlimBuilder(args).Build();

app.MapGet("/", () => Html(
    Head(
        Title("CSX Sample"),
        Meta(charset("utf-8"))),
    Body(
        Div(@class("container"),
            H1(@class("text-center"), "CSX Sample"),
            A(href("/todos"), "Todos")))).ToResponse());

app.MapGet("todos", () => TodosPage(samples).ToResponse());
app.MapPost("todos", ([AsParameters] TodoRequest form) => TodosPage(
    samples = [new Todo
    {
        Id = samples.Max(t => t.Id) + 1,
        Title = form.Title,
        DueBy = form.DueBy,
        IsComplete = form.IsCompleteBool
    }, ..samples]).ToResponse()
).DisableAntiforgery();

app.Run();

class TodoRequest
{
    [FromForm] public required string Title { get; set; }
    [FromForm] public DateTime? DueBy { get; set; } = default;
    [FromForm] public string? IsComplete { get; set; } = "off";
    public bool IsCompleteBool => IsComplete == "on";
}