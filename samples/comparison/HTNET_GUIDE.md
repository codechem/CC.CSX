# htnet (CC.CSX.Browser) — primer for this task

You are building a **client-side .NET WASM** app with **CC.CSX**, a C# library that
generates HTML from pure C# function calls (think JSX/Hiccup, but C#). There is **no
Blazor, no Razor, no .cshtml** — you write C# that returns an `HtmlNode` tree, and
`CC.CSX.Browser` renders it into the page and **morphs the live DOM** on every update.
Read this whole file before writing code; the API is small but unfamiliar.

## How an app is structured

Everything goes in **`Program.cs`** (top-level statements). The shape is:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using CC.CSX;
using CC.CSX.Browser;
using static CC.CSX.HtmlElements;          // Div, Span, Table, Button, Select, ...
using static CC.CSX.HtmlAttributes;         // id, @class, style, value, type, ...
using static CC.CSX.Browser.HtmlEvents;     // onClick, onChange, onInput, ...

// 1) mutable in-memory state lives as plain variables/fields
int count = 0;

// 2) mount the view, then idle forever
await BrowserApp.MountAsync("#app", View);
await BrowserApp.RunAsync();

// 3) the view is a function returning a single root HtmlNode
HtmlNode View() =>
    Div(@class("app"),
        Button(onClick(() => count++), $"Clicked {count}")
    );
```

That's the whole lifecycle. `MountAsync` renders `View()` into `#app`; `RunAsync()`
keeps the WASM app alive.

## The golden rule: state + auto re-render

- Keep app state in **ordinary C# variables** (captured by the `View()` closure) or
  static fields. To build a screen from state, just **read those variables inside
  `View()`**.
- **Event handlers mutate the state, and the framework automatically re-renders**
  after every delegate handler runs. You do **not** need to call anything after a
  click/change — `onClick(() => count++)` updates the screen by itself.
- (If you ever mutate state *outside* an event handler, e.g. a timer, call
  `BrowserApp.Refresh()` to re-render. You won't need that here.)
- Re-render morphs the DOM in place, so input focus/caret survive.

## Elements (factories from `HtmlElements`, PascalCase)

Each HTML element is a method taking `params HtmlItem[]` — you pass **attributes and
children mixed together, in any order**:

```csharp
Div(@class("card"), id("x"), H1("Title"), P("body"))
Section(...), Header(...), Main(...), Span(...), Button(...), Select(...), Option(...),
Table(...), Thead(...), Tbody(...), Tr(...), Th(...), Td(...), H1(...)..H3(...), P(...), Ul(...), Li(...)
```

Strings/ints/etc. passed as children become text nodes (auto-escaped). Example:
`Td(@class("num"), $"${amount:N0}")`.

## Attributes (factories from `HtmlAttributes`, lowercase)

```csharp
id("main")
@class("card", "kpi")          // params string[] -> joined with spaces -> class="card kpi"
@class(active ? "filter-tab active" : "filter-tab")   // conditional classes: build the string
style("height:42%")            // raw inline style string  (this is the ONLY inline style you need)
value("Last 7 days")           // e.g. on <select> to reflect current selection
type("button")
("data-foo", "bar")            // a (string,string) tuple also works as an attribute
```

Note `@` prefix on `@class` (because `class` is a C# keyword). `style(...)` here takes a
plain CSS string — use `style($"height:{pct}%")` for the bars.

## Events (from `CC.CSX.Browser.HtmlEvents`)

Handlers are C# delegates. Each event has `Action`, `Action<DomEvent>`, and async
overloads:

```csharp
Button(onClick(() => selected = "Paid"), "Paid")          // no-arg
Button(onClick(e => sortDir = -sortDir), "Amount")        // with event
Select(onChange(e => { range = e.Value!; Reseed(); }),     // <select> change
    Option(value("Today"), "Today"),
    Option(value("Last 7 days"), "Last 7 days"))
```

`DomEvent` gives you what you need here: **`e.Value`** (string? — the value of the
changed `<input>`/`<select>`), plus `e.Checked`, `e.Key`, `e.TargetId`, etc.
For a `<select>`, read the chosen option from `e.Value` in `onChange`.
Remember: after the handler runs, the view re-renders automatically.

## Rendering dynamic lists / collections

Build child arrays with LINQ and splat them in. A node accepts an `HtmlNode[]` as a
child (it becomes a fragment):

```csharp
Tbody(
    orders.Where(o => filter == "All" || o.Status == filter)
          .Select(o => Tr(
              Td(@class("mono"), $"#{o.Id}"),
              Td(o.Customer),
              Td(Span(@class("badge", o.Status.ToLower()), o.Status)),
              Td(@class("num"), $"${o.Amount:N0}")
          ))
          .ToArray()
)
```

The bar chart row is the same idea: `.Select(d => Div(@class("bar-col"),
Div(@class("bar"), style($"height:{d.Pct}%")), Span(@class("bar-label"), d.Label)))
.ToArray()`.

Conditionals: use normal C# ternaries. For "show empty row when no matches", compute
the filtered list first, then return either the rows or a single
`Tr(Td(("colspan","4"), @class("empty"), "No orders"))`.

## Things specific to this codebase

- Project file, `index.html`, `main.js`, the WASM boot, and `dashboard.css` are
  **already wired** — don't touch them. You only write `Program.cs`.
- `#app` is the mount point; `dashboard.css` is already linked in `index.html`.
- Use only the CSS classes from `dashboard.css` (see SPEC + STYLEGUIDE). The only
  inline style allowed is the bar `height:NN%`.
- Build/run with `dotnet run` from the project dir (it's a `Microsoft.NET.Sdk.WebAssembly`
  project). To just check it compiles: `dotnet build`.
- There is no `Console`-style UI; everything is the `HtmlNode` tree returned by `View()`.

## Minimal end-to-end example (counter + select + list) — for shape only

```csharp
using System; using System.Collections.Generic; using System.Linq;
using CC.CSX; using CC.CSX.Browser;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;
using static CC.CSX.Browser.HtmlEvents;

string range = "Last 7 days";
List<int> data = [3, 7, 5, 9];

await BrowserApp.MountAsync("#app", View);
await BrowserApp.RunAsync();

HtmlNode View() =>
    Div(@class("app"),
        Select(value(range), onChange(e => range = e.Value!),
            Option(value("Today"), "Today"),
            Option(value("Last 7 days"), "Last 7 days")),
        Div(@class("chart"),
            data.Select(v => Div(@class("bar-col"),
                Div(@class("bar"), style($"height:{v * 10}%")))).ToArray())
    );
```

Now read `SPEC.md` and implement Pulse in `Program.cs`.
