using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;

Print(
  Div(style("background:silver;"),
    "Hello HTML",
    H1("Hello world"),
    Article(id("article-1"),
      P("Some content here"),
      Button(id("something"), onclick("alert('hello world')")))));
void Print(object node) => Console.WriteLine(node.ToString());