# htnet vs Blazor

Both htnet (CC.CSX) and Blazor let you build web UIs in C# without writing
HTML templates by hand. They take opposite approaches to get there:

- **htnet**: HTML is a plain C# *value*. A view is a function that returns an
  `HtmlNode`; rendering is a single pass that streams it to the response.
- **Blazor**: HTML is produced by a *component framework*. A view is a
  `ComponentBase` with a lifecycle, parameters, and a render tree that gets
  diffed and re-rendered.

Neither is "better" — they solve different problems. This page shows where
each shines so you can pick deliberately.

## Server-side rendering performance

The repo ships a head-to-head benchmark
([`tests/CC.CSX.Benchmarks/BlazorComparison.cs`](https://github.com/codechem/CC.CSX/blob/main/tests/CC.CSX.Benchmarks/BlazorComparison.cs))
that renders the *same page* through both stacks. The Blazor side is a
`ComponentBase` written directly against `RenderTreeBuilder` — exactly what a
`.razor` file compiles to — rendered through `HtmlRenderer`, the engine behind
Blazor SSR. So the comparison measures the frameworks, not the markup.

| Method              | Mean      | Ratio | Allocated | Alloc Ratio |
|-------------------- |----------:|------:|----------:|------------:|
| htnet (`WriteTo`)   |   2.1 µs  |  1.00 |  12.95 KB |        1.00 |
| Blazor `HtmlRenderer` |  11.3 µs |  5.41 |  16.40 KB |        1.27 |

*(.NET 10, AMD Ryzen 7 5700U, BenchmarkDotNet. Reproduce with
`dotnet run -c Release --project tests/CC.CSX.Benchmarks -- --filter "*BlazorComparison*"`.)*

htnet renders the page about **5× faster** with **27% fewer allocations** and
no Gen1 collections. The gap is architectural: Blazor builds a render tree,
dispatches through its `Dispatcher`, and walks the tree to emit HTML; htnet
writes the nodes straight to the `TextWriter`.

## Architecture, side by side

| Dimension | htnet (CC.CSX) | Blazor |
|---|---|---|
| View model | Plain C# values (`HtmlNode`) returned by functions | Components with lifecycle, parameters, and a `.razor` DSL compiled by source generators |
| Server interactivity | Stateless request/response via [htmx](https://htmx.org/) — no per-user server state | Blazor Server holds a SignalR circuit and a live component tree in memory per connected user |
| Client-side | `CC.CSX.Browser`: the same view code in .NET WASM, DOM morphing, C# delegate events — no framework layer | Blazor WebAssembly: the full component framework on top of the WASM runtime |
| Testing | `Assert.Equal(expected, node.ToString())` — views are values | bUnit, render contexts, component fixtures |
| Composition | Plain C#: LINQ, ternaries, helper methods, `params` | `@foreach` / `@if` template syntax, `RenderFragment` |
| Debugging | Breakpoint anywhere; no generated code between you and the output | Stepping through generated render-tree code |
| CSS | Typed sidecar: `.css` files become compile-time constants (`CC.CSX.Css`), typed Tailwind (`CC.CSX.Css.Tailwind`) | CSS isolation (scoped `.razor.css`), string class names |
| Hot reload | `dotnet watch` hot reload + automatic browser refresh (see below) | `dotnet watch` hot reload + automatic browser refresh |
| Ecosystem | htmx + anything that speaks HTML | MudBlazor, Telerik, Radzen, first-party Microsoft docs and tooling |

## Hot reload: it's just C#

Because htnet views are ordinary C# methods, the standard .NET inner loop
works unmodified — verified end-to-end against `samples/Web`:

- Edit the body of a view method while `dotnet watch` is running and the
  change is **hot-patched into the live process** (`Hot reload succeeded`,
  no restart) — the next request serves the new markup.
- `dotnet watch` injects its browser-refresh script into htnet's streamed
  responses (in the `Development` environment), so an **open browser tab
  reloads itself** the moment the patch lands. No manual refresh.
- Since a view is re-evaluated per request, every hot reload is a clean
  render — there is no live component state to patch or reconcile.

The usual C# hot-reload rules apply: editing an existing route's lambda body
patches in place, while *adding* a new `MapGet`/`MapPost` only runs at
startup, so `dotnet watch` falls back to an automatic rebuild-and-restart
(a few seconds).

## When Blazor is the better choice

Honesty makes the rest of this page credible:

- **Rich, stateful client UIs** — dashboards, editors, drag-and-drop. Blazor's
  component model and state handling are built for this; htnet + htmx is
  built for hypermedia-style apps.
- **Component library ecosystem** — MudBlazor, Telerik, Radzen ship hundreds
  of polished components. htnet gives you HTML and functions.
- **Forms, validation, auth scaffolding** out of the box.
- **Designer-friendly markup** — you can paste HTML from a mockup straight
  into a `.razor` file. In htnet you translate it into `Div(...)` calls.
- **Team familiarity and hiring** — Blazor is a Microsoft product with a
  large community.

## When htnet is the better choice

- You want **server-rendered, htmx-style apps** that scale like a stateless
  API — no SignalR circuits, no per-user server memory.
- You want views that are **values**: composable with LINQ, unit-testable
  with a string compare, no component lifecycle.
- You care about **render throughput and allocations** on hot paths.
- You want **zero magic**: no template compiler, no generated code, breakpoints
  and refactoring work everywhere — it's just C# (or F#).
- You want one view definition that can render **on the server or in the
  browser** (`CC.CSX.Browser`), with htmx attributes handled in either place.
