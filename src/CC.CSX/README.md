# CC.CSX Package
- [Repo Link](https://github.com/codechem/CC.CSX)

Provides the ability to represent and render HTML code in declarative fashion

The idea is to have strongly typed and readable structure,
for the developer to be able to easily navigate and manipulate the output similar like [JSX](https://legacy.reactjs.org/docs/introducing-jsx.html) in the javascript world.

By using implicit operators there is not need to use `new HtmlNode`,
or unnecessary quotes and brackets, so the layout is easily readable.

An Example:

```csharp
Div((style, "background:silver;"),
  "Hello world small",
  H1("Hello world"),
  Article((id, "article-1"),
    P("Some content here")))
```

## How to use

Main usage would be as a Html Response builder.

```csharp
app.MapGet("/test", () => Results.Extensions.Html(MainPage(
    Menu(A("Home", (hxGet, "https://codechem.com")),
        A("About", (href, "/about")),
        A("Contact", (href, "/contact"))),
    Article(
        H1("Hello, World!", 
          (hxGet, $"/api/1/halicea/short-meeting/occupied/{DateTime.Now.ToString("yyyy-MM-dd")}"),
          (hxSwap, "outerHTML"),
          (hxTarget, "#results")),
        Button("Click me!", (hxGet, "/api/1/halicea/short-meeting/occupied/2021-10-10"),
          (hxSwap, "outerHTML"),
          (hxTarget, "#results")),
        P("Welcome to your new app."),
        Code((id, "results")),
        P("This is a test of the new CC.CSX library."))
    )));
```

Because just by using pure methods, in the style of JSX.
Future work will include optimizations and performance improvements.
Code: github.com/codechem/cc.csx

## How it works

As you may have noticed, there is no type declaration anywhere, but that does
not mean we are not using strong types.

The strings, and tuples are being used in the
example above are converted to `HtmlAttribute`, and `HtmlNode` through implicit operators.
instances so proper serialization can be performed.

Contributions or ideas are welcome.

With 💚 from [CodeChem](https://www.codechem.com)
