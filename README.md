# CC.CSX Package

### Links
- [Repo Link](https://github.com/codechem/CC.CSX)
# About
*`CC.CSX`* provides the ability to define and render HTML structure in 
a declarative fashion by just using pure C#.

The idea is to have strongly typed and readable structure,
for the developer to be able to easily navigate and manipulate the output, 
similar like [JSX](https://legacy.reactjs.org/docs/introducing-jsx.html) 
in the JS world.

By using implicit operators there is not need to use `new HtmlNode`,
or unnecessary quotes and brackets, so the layout is easily readable.

- Any attribute is a tuple of two strings(key and the value), 
  - `using static CC.CSX.HtmlAttributeKeys` imports all the attribute 
    keys existing in Html
- Every Html element has its defined method with the same name as the Element
  - `using CC.CSX.HtmlElements` imports all the methods that create Html Elements.
    method that can be used

Take a look at the following example:

```cs
Div((style, "background:silver;"),
  "Hello HTML",
  H1("Hello world"),
  Article((id, "article-1"),
    P("Some content here")))
```
or also the alternative flavor if you like it more.
(Instead of using the import `using static CC.CSX.HtmlAttributeKeys` if you import
`using static CC.CSX.HtmlAttributes` you will get the same html output with)

```cs
Div(style("background:silver;"),
  "Hello HTML",
  H1("Hello world"),
  Article(id("article-1"),
    P("Some content here")))
```

and finally, the result is following:

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


## How to use

Main usage would be as a Html Response builder.

*For this you also need to install `CC.CSX.Web` package from Nuget in order to 
have the `ToResult()` extension available.*

```cs
app.MapGet("/test", () => MainPage(
  Menu(
    A((hxGet, "https://codechem.com"), "Home"),
    A((href, "/about"), "About"),
    A((href, "/contact"), "Contact")),
  Article(
    H1("Hello, World!",
      (hxGet, $"/api/1/halicea/short-meeting/occupied/{DateTime.Now.ToString("yyyy-MM-dd")}"),
      (hxSwap, "outerHTML"),
      (hxTarget, "#results")),
    Button("Click me!",
      (hxGet, "/api/1/halicea/short-meeting/occupied/2021-10-10"),
      (hxSwap, "outerHTML"),
      (hxTarget, "#results")),
    P("Welcome to your new app."),
    Code((id, "results")),
    P("This is a test of the new CC.CSX library."))).ToResult());
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
