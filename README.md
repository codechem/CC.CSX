# CC.CSX 
[![build](https://github.com/codechem/CC.CSX/actions/workflows/build.yml/badge.svg)](https://github.com/codechem/CC.CSX/actions/workflows/build.yml)


## About

*`CC.CSX`* provides the ability to define and generate HTML output in
a declarative fashion by just using pure C# or F# or other .Net based language.

The idea is to define strongly typed readable and ergonomic structures of HTML components, elements and attributes in a way that is similar to the way you would write HTML, but in a more structured and type-safe way.

This way the developer is able to easily organize, navigate and manipulate the final output.


It is similar like [JSX](https://legacy.reactjs.org/docs/introducing-jsx.html)
in the JS world, or even more similar to [hiccup](https://github.com/weavejester/hiccup) in `clojure`.


## How to use it

Main usage would be as Html Renderer, you can build entire pages, components and applications with it wihout the need to write any HTML(or JS).

For this you also need to install the [CC.CSX.Web](https://www.nuget.org/packages/CC.CSX.Web) package from Nuget in order to
have the Render method available. 

You may also need the [CC.CSX.Htmx](https://www.nuget.org/packages/CC.CSX.Htmx) package which provides the [Htmx](https://htmx.org/) related attributes.
This way you can build reactive applications with ease and without the need to write any `JS` or `HTML`.

Bellow you can find a complete version of the legendary `Counter` example, but this time in `C#`, `Asp.Net` using , `Dotnet Minimal APIs` and this library `CC.CSX`.

```cs
var builder = WebApplication.CreateBuilder(args); //classic bootstrapping
var app = builder.Build(); // create the app

int counter = 0;
app.MapGet("/", () => Render( // Render the Html node
    Master("Counter", // Master layout is just a method defined bellow in the code
        //we use hxPost to send a post request and update the counter with the new value
        Button("-", hxPost("/decrement", target: "#counter")), 
        Label(id("counter"), counter),
        Button("+", hxPost("/increment", target: "#counter"))
    )
));

// These are the post routes that will be called when the buttons 
// are clicked and will just return the new counter value
app.MapPost("/increment", () => Render($"{++counter}"));
app.MapPost("/decrement", () => Render($"{--counter}"));
app.Run();

// This method provides the master layout for the page and can be reused
static HtmlNode Master(string title, params HtmlNode[] content) 
=> Html(
    Meta(charset("utf-8")),
    Head(
        Title("Htnet Demo"),
        Meta(charset("utf-8")),
        HtmxImports),
    Body(
        H1(@class(Site.title), title), // prefer typed classes from the CSS generator (see "Styling" below)
        content, // this is the content that will be rendered as a child of the body
        Hr()
    )
);
```
## How it works

As you may have noticed, there is no type declaration anywhere, but that does
not mean we are not using strong types.
The `strings`, and tuples are being used in the example above,
are converted to `HtmlAttribute`, and `HtmlNode` through implicit operators.

There are quite a few implicit operators that are used to convert the types into proper `HtmlNode` or `HtmlAttribute` instances.
This is what makes the whole declarative structure possible.

Most of the Html elements and attributes can be created by their static method counterparts(`Div(...)`, `H1(...)`, `style(...)`, `id(...)`, etc.).
methods that return `HtmlNode` or `HtmlAttribute` instances.

Every HTML node has its defined method with the same name as the Element
  - `using static CC.CSX.HtmlElements` imports all the methods that create HTML Nodes.
  - `using static CC.CSX.HtmlAttributes` imports all the methods that create HTML Attributes.

Some more notable implicit operators are:
- Any parameter that is a tuple of two strings (key and the value) is converted to `HtmlAttribute`
- Any `string`, `int`, `float`, `double` or `bool` parameter is converted to `HtmlTextNode` which is a special node that just contains then text representation of the value.
- An array of `HtmlNode` is converted to `HtmlFragment` which is a special node that contains a list of nodes.
- An array of `HtmlAttribute` is converted to `MultiAttr` which is a special attribute that contains a list of attributes.


Take a look at the following example:

```cs
Div((style, "background:silver;"), /* attribute of the div */
  "Hello HTML", /* text node*/
  H1("Hello world"), /* h1 node with text node inside */
  Article((id, "article-1"), 
    P("Some content here")
  )
)
```

This will generate the following HTML:

```html
<div style="background:silver;">
  Hello HTML
  <h1>
    Hello world
  </h1>
  <article id="article-1">
    <p>
      Some content here
    </p>
  </article>
</div>
```
For existing HTML elements and attributes, you can use the static methods provided by the `HtmlElements` and `HtmlAttributes` classes, if you need to create custom elements you can use the new HtmlNode constructor, and tuple for attributes.

## Styling and CSS classes

**The preferred way to author styles and classes is with `CC.CSX.Css` and its source generator —
not raw class strings.** Write your CSS in a `.css` file, register it as an additional file, and the
generator turns it into compile-checked, refactor-safe, discoverable typed members:

```cs
// site.css is registered via <AdditionalFiles Include="styles/*.css" /> and the
// CC.CSX.Css.Generator (shipped with the CC.CSX.Css package).
using static CC.CSX.Css.CssImports; // Inline(...) / StyleSheet(...)
using static MyApp.Css;             // generated: site.css -> Site.<className> + Site.Bundle

Html(
    Head(Inline(Site.Bundle)),                  // serve the generated stylesheet
    Body(
        Div(@class(Site.container),             // typed class constant, not the raw string "container"
            H1(@class(Site.title), "Title"))
    )
);
```

- **Typed CSS classes** from your `.css` files via the generator — the recommended default.
- **`CssProperties`** for typed inline styles: `style(background("silver"), padding(8.px()))`.
- **`CC.CSX.Css.Tailwind`** (`Tw.*`) for typed Tailwind utility classes.
- Plain class strings still work everywhere (`CssClass` ⇄ `string`), but reserve them for one-off or
  third-party class names.

See `samples/Web` and `samples/CalendarSample` for the end-to-end `.css` generator setup.

## How it compares to Blazor

Both let you build web UIs in C#, but htnet treats HTML as a plain C# *value* (a function returning `HtmlNode`) while Blazor runs a full component framework. The repo ships a head-to-head benchmark (`tests/CC.CSX.Benchmarks/BlazorComparison.cs`) rendering the same page through both — the Blazor side uses `RenderTreeBuilder` + `HtmlRenderer`, exactly what `.razor` SSR compiles down to:

| Method                | Mean     | Ratio | Allocated |
|---------------------- |---------:|------:|----------:|
| htnet (`WriteTo`)     |  2.1 µs  |  1.00 |  12.95 KB |
| Blazor `HtmlRenderer` | 11.3 µs  |  5.41 |  16.40 KB |

About **5× faster with 27% fewer allocations** — and because views are stateless functions paired with [htmx](https://htmx.org/), there are no per-user SignalR circuits to hold in server memory.

Since views are ordinary C# methods, `dotnet watch` hot reload works out of the box: edit a view body and the running process is patched in place while the open browser tab refreshes itself — no restart, no Razor compiler in between.

See [docs/articles/htnet-vs-blazor.md](docs/articles/htnet-vs-blazor.md) for the full comparison, including an honest "when Blazor is the better choice" section.

### TODO

- [ ] Project template
  - that scaffolds a new project with all the needed packages

- [ ] WebAssembly support

- [ ] Think of a better name

- [ ] Performance optimizations
  - Use span instead of strings.
  - Compile a template and just compute the dynamic parts when needed.

- [ ] Reactive features
  - Ability to process events directly from C# instead of using JS.
    - Server side and client side
    - Some form of state management
    - More native HTMX integration 

- [ ] Better documentation and examples

- [ ] Native CSS support
    - Ability to define CSS in C# and have it compiled to CSS
    - Maybe port Tailwind CSS to C#
    - Do somethig like [Stitches](https://stitches.dev/) but in C#

- [ ] Library for building mobile apps on top of MAUI

Public Code is here: https://github.com/codechem/cc.csx


## Links

- [Repo Link](https://github.com/codechem/CC.CSX)

There are three packages packaged in this repo:

- [CC.CSX](https://www.nuget.org/packages/CC.CSX) providing the core
functionality explained bellow in this document

- [CC.CSX.Web](https://www.nuget.org/packages/CC.CSX.Web) useful extensions
for using the core package in ASP.NET Core

- [CC.CSX.Htmx](https://www.nuget.org/packages/CC.CSX.Htmx) collection of
attribute methods for [HTMX](https://htmx.org/)

Contributions and ideas are welcome.
