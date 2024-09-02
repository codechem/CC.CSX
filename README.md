# CC.CSX 
[![build](https://github.com/codechem/CC.CSX/actions/workflows/build.yml/badge.svg)](https://github.com/codechem/CC.CSX/actions/workflows/build.yml)

## Links

- [Repo Link](https://github.com/codechem/CC.CSX)

There are three packages packaged in this repo:

- [CC.CSX](https://www.nuget.org/packages/CC.CSX) providing the core
functionality explained bellow in this document
- [CC.CSX.Web](https://www.nuget.org/packages/CC.CSX.Web) useful extensions
for using the core package in ASP.NET Core
- [CC.CSX.Htmx](https://www.nuget.org/packages/CC.CSX.Htmx) collection of
attribute methods for [HTMX](https://htmx.org/)

## About

*`CC.CSX`* provides the ability to define and generate, a HTML structure in
a declarative fashion by just using pure C# or F# (or Powershell if you are so dirty).

The idea is to have strongly typed and readable structure,
so the developer is able to easily navigate and manipulate the final output.
It is similar like [JSX](https://legacy.reactjs.org/docs/introducing-jsx.html)
in the JS world, or even more similar to [hiccup](https://github.com/weavejester/hiccup) in `clojure`.

- Any attribute is a tuple of two strings (key and the value),
  - `using static CC.CSX.HtmlAttributeKeys` imports all the attribute
    keys existing in HTML

- Every HTML element has its defined method with the same name as the Element
  - `using CC.CSX.HtmlElements` imports all the methods that create HTML Elements.

Take a look at the following example:

```c
Div((style, "background:silver;"),
  "Hello HTML",
  H1("Hello world"),
  Article((id, "article-1"),
    P("Some content here")))
```

You can also use the alternative flavor if you like it more. Here instead of tuples you can use methods for the attributes, this gives you better autocomplete.
(Instead of using the import `using static CC.CSX.HtmlAttributeKeys` if you import
`using static CC.CSX.HtmlAttributes` you will get the same html output).
In case there is some custom attribute the you want to use, just
use a tuple pair `("hello", "attribute")`.

```cs
Div(style("background:silver;"),
  "Hello HTML",
  H1(@class("world"), 
    "Hello world"),
  Article(id("article-1"),
    P("Some content here")))
```

Finally, the result is following:

```html
<div style="background:silver;">
  Hello HTML
  <h1 hello="world">
    Hello world
  </h1>
  <article id="article-1">
    <p>
      Some content here
    </p>
  </article>
</div>
```

## How to use

Main usage would be as Html Renderer, you can build entire pages, components and applications with it wihout the need to write any HTML(or JS).

For this you also need to install the [CC.CSX.Web](https://www.nuget.org/packages/CC.CSX.Web) package from Nuget in order to
have the Render method available. 

You may also need the [CC.CSX.Htmx](https://www.nuget.org/packages/CC.CSX.Htmx) package which provides the [Htmx](https://htmx.org/) related attributes.
This way you can build reactive applications with ease and without the need to write any `JS` or `HTML`.

Bellow you can find a complete version of the legendary `Counter` example, but this time in `C#`, `Asp.Net using`, `Minimap API` and `CC.CSX` with `HTMX`.

```cs
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

int counter = 0;
app.MapGet("/", () => Render(
    Master("Counter",
        Button("-", hxPost("/decrement", target: "#counter")),
        Label(id("counter"), counter),
        Button("+", hxPost("/increment", target: "#counter"))
    )
));

// We just return the number 
app.MapPost("/increment", () => Render(++counter));
app.MapPost("/decrement", () => Render(--counter));
app.Run();

// This method provides the master layout for the page and can be reused
static HtmlNode Master(string title, params HtmlNode[] content) => Html(
    Meta(charset("utf-8")),
    Head(
        Title("Htnet Demo"),
        Meta(charset("utf-8")),
        HtmxImports),
    Body(
        H1(@class("text-center"), title),
        content,
        Hr()
    )
);
```

### Future work

- Think of a better name

- Performance optimizations
  - Use span instead of strings.
  - Compile a template and just compute the dynamic parts when needed.

- Reactive features
  - Ability to process events directly from C# instead of using JS.
    - Server side and client side
    - Some form of state management
    - More native HTMX integration 

- Project template
  - that scaffolds a new project with all the needed packages

- Better documentation and examples
- Native CSS support
- Library for building mobile apps on top of MAUI

Public Code is here: https://github.com/codechem/cc.csx

## How it works

As you may have noticed, there is no type declaration anywhere, but that does
not mean we are not using strong types.
The `strings`, and tuples are being used in the example above,
are converted to `HtmlAttribute`, and `HtmlNode` through implicit operators.
But anyway, most of the Html elements and attributes are simple static
methods that return `HtmlNode` or `HtmlAttribute` instances.

Contributions or ideas are welcome.

With 💚 from [CodeChem](https://www.codechem.com)
