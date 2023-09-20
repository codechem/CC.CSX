using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;

Print(
  Div(style("background:silver;"),
    "Hello HTML",
    Fragment(Label("Some text"), H1("some header")),
    H1("agrgaer",
       style("background: silver;"), "Hello world"),
    Article(id("article-1"),
      P("Some content here"),
      Button(id("bang"), onclick("alert('hello world')")))));










void Print(object node) => Console.WriteLine(node.ToString());