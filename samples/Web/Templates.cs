namespace Web;
using CC.CSX;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;

public static class Templates
{
    public static HtmlNode TodosPage(IEnumerable<Todo> todos) =>
        Html(
            Meta(charset("utf-8")),
            Body(
                H1(@class("text-center"), "Sample todos"),
                TodoForm(),
                Ul(todos.Select(TodoView).ToArray())));

    public static HtmlNode TodoForm() =>
        Form(method("post"), action("/todos"), enctype("multipart/form-data"),
            Input(type("text"), name("Title")),
            Input(type("date"), name("DueBy")),
            Label("Is complete?"),
            Input(type("checkbox"), name("IsComplete")),
            Button("Create"),
            Hr());

    public static HtmlNode TodoView(Todo todo) =>
        Li(
            Label($"Name: {todo.Title}"),
            Label($"Date: {todo.DueBy?.ToString("MM-dd HH:mm") ?? "N/A"}"),
            todo.IsComplete ? Label(style("color:green"), "✅") : None);
}