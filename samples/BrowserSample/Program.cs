using System;
using System.Collections.Generic;
using System.Linq;
using CC.CSX;
using CC.CSX.Browser;
using CC.CSX.Css;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;
using static CC.CSX.Browser.HtmlEvents;
using static CC.CSX.Htmx.HtmxAttributes;
using static CC.CSX.Css.CssAttributes;
using static CC.CSX.Css.CssProperties;

int count = 0;
string userName = "";
bool hovering = false;
List<string> todos = ["Ship CC.CSX.Browser"];

// stable named action, htmx-route style
BrowserApp.Map("counter/reset", () => count = 0);

// hx routes handled entirely in the browser — same hxPost/hxDelete markup
// would work against a real server with htmx.js
BrowserApp.MapPost("/todos", req =>
{
    var title = req["title"]?.Trim();
    if (string.IsNullOrEmpty(title)) return null;
    todos.Add(title);
    return TodoItem(title);
});
BrowserApp.MapDelete("/todos/{title}", req =>
{
    todos.Remove(req["title"]!);
    return CC.CSX.Fragment.Empty; // hx-swap="delete" removes the target regardless
});

await BrowserApp.MountAsync("#app", View);
await BrowserApp.RunAsync();

HtmlNode View() =>
    Div(style(
            fontFamily("sans-serif"), maxWidth(32.rem()),
            margin("3rem auto"), display("flex"),
            flexDirection("column"), gap(1.5.rem())),
        H1("htnet in the browser"),
        P("Rendered client-side by CC.CSX in .NET WASM; events are C# delegates and named actions."),
        Div(
            P(style(fontSize(1.5.rem())), $"Count: {count}"),
            Button(onClick(() => count++), "Increment"),
            " ",
            Button(htAction("counter/reset"), "Reset")
        ),
        Div(
            Input(placeholder("Type your name…"), value(userName),
                  onInput(e => userName = e.Value ?? "")),
            P($"Hello, {(userName.Length == 0 ? "stranger" : userName)}! ({userName.Length} chars)")
        ),
        Div(onMouseover(() => hovering = true),
            onMouseout(() => hovering = false),
            style(
                padding(1.rem()), borderRadius(8.px()),
                background(hovering ? "#86efac" : "#e5e7eb"),
                transition("background .2s")),
            hovering ? "Hovering — handled in C#!" : "Hover me"
        ),
        Div(
            H3("Todos — hx routes, no server"),
            Form(hxPost("/todos"), hxTarget("#todo-list"), hxSwap("beforeend"),
                Input(name("title"), placeholder("Add a todo…")),
                " ",
                Button("Add")),
            Ul(id("todo-list"), todos.Select(TodoItem).ToArray())
        )
    );

HtmlNode TodoItem(string title) =>
    Li(title, " ",
        Button(hxDelete($"/todos/{Uri.EscapeDataString(title)}"),
               hxTarget("closest li"), hxSwap("delete"), "✕"));
