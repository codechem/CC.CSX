using CC.CSX;
using CC.CSX.Browser;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;
using static CC.CSX.Browser.HtmlEvents;

int count = 0;
string name = "";
bool hovering = false;

// stable named action, htmx-route style
BrowserApp.Map("counter/reset", () => count = 0);

await BrowserApp.MountAsync("#app", View);
await BrowserApp.RunAsync();

HtmlNode View() =>
    Div(new HtmlStyleAttribute(
            ("font-family", "sans-serif"), ("max-width", "32rem"),
            ("margin", "3rem auto"), ("display", "flex"),
            ("flex-direction", "column"), ("gap", "1.5rem")),
        H1("htnet in the browser"),
        P("Rendered client-side by CC.CSX in .NET WASM; events are C# delegates and named actions."),
        Div(
            P(style("font-size:1.5rem"), $"Count: {count}"),
            Button(onClick(() => count++), "Increment"),
            " ",
            Button(htAction("counter/reset"), "Reset")
        ),
        Div(
            Input(("placeholder", "Type your name…"), ("value", name),
                  onInput(e => name = e.Value ?? "")),
            P($"Hello, {(name.Length == 0 ? "stranger" : name)}! ({name.Length} chars)")
        ),
        Div(onMouseover(() => hovering = true),
            onMouseout(() => hovering = false),
            new HtmlStyleAttribute(
                ("padding", "1rem"), ("border-radius", "8px"),
                ("background", hovering ? "#86efac" : "#e5e7eb"),
                ("transition", "background .2s")),
            hovering ? "Hovering — handled in C#!" : "Hover me"
        )
    );
