using CC.CSX;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributeKeys;
using static CC.CSX.HtmxAttributeKeys;

Print(Template(
  Script(@"
        function hello() {
           alert('hello');
        }
        // window.addEventListener('load', hello);"),
  Div(
    H1("Hello world", "Zdravo"),
    MainConent(),
    A((hxGet, "/do-something"), (@class, "test"), (style, "color: red;font:bold;"), "Lets go"),
    Span((style, "color: red;"), "Hello world"))));

HtmlNode Scripts(params HtmlNode[] children) => Head(children);

HtmlNode MainConent() => Article((id, "article-1"),
    (hxGet, "/articles/1"),
    (hxSwap, "outerHTML"),
    (hxTrigger, "load"),
    (hxTarget, "#article-1"),
    "Text",
    P("This is a paragraph"),
    P("This is another paragraph"),
    P("This is a third paragraph"));
HtmlNode Template(HtmlNode head, HtmlNode root, string mode = "light") =>
  Html((style, mode is "dark" ? "background-color: black" : "background-color: white;"),
    Scripts(head),
    Body(root));

void Print(HtmlNode node) => Console.WriteLine(node.ToString());