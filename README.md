# CC.CSX Package

Provides the ability to represent and render HTML code in declarative fashion

```csharp
Div((style, "background:silver;"),
  "Hello world small",
  H1("Hello world"),
  Article((id, "article-1"),
    P("Some content here")))
```

Build HTML, just by using pure methods, in the style of JSX.
Future work will include optimizations and performance improvements.
Code: github.com/codechem/cc.csx

Contributions are welcome.

With love,
CodeChem team
