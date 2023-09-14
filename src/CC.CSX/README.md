# CC.CSX Package

Provides the ability to represent and render HTML code in declarative fashion

The idea is to have strongly typed and readable structure,
for the developer to be able to easily navigate and manipulate the output.

There are no type definitions, no unnecessary quotes, so the structure
can be easily readable.

An Example:

```csharp
Div((style, "background:silver;"),
  "Hello world small",
  H1("Hello world"),
  Article((id, "article-1"),
    P("Some content here")))
```

## Why CC.CSX

Since the ideas is very similar to JSX

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

</hr>

Because just by using pure methods, in the style of JSX.
Future work will include optimizations and performance improvements.
Code: github.com/codechem/cc.csx

## How it works
As you may have noticed, there is no type declaration anywhere, but that does 
not mean we are not using strong types. 

The strings, and tuples are being used in the example above are converted to `HtmlAttribute`, and `HtmlNode` through implicit operators.
instances so proper serialization can be performed.


Contributions are welcome.

With love,
CodeChem team
