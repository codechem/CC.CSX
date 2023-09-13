using static CC.CSX.CustomElements;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributeKeys;
using static CC.CSX.HtmlElementEvents;
using static CC.CSX.HtmxAttributeKeys;

namespace CC.CSX.Examples;

public class Example
{
    public HtmlNode Node => Template(
      Script(@"
        function hello() {
           alert('hello');
        }
        window.addEventListener('load', hello);"),
      Div(
        H1("Hello world"),
        Article((id, "article-1"),
                (hxGet, "/articles/1"),
                (hxSwap, "outerHTML"),
                (hxTrigger, "load"),
                (hxTarget, "#article-1"),
          P("This is a paragraph"),
          P("This is another paragraph"),
          P("This is a third paragraph")),
        A((hxGet, "/do-something"), (onClick, "alert('hello')"), (@class, "test"), (style, "color: red;"), "Lets go"),
        Span((style, "color: red;"), "Hello world")));

    HtmlNode Scripts(params HtmlNode[] children) => Head(children);
    HtmlNode Template(HtmlNode head, HtmlNode root, string mode = "dark") =>
        Html((style, mode is "dark" ? "background-color: black" : "background-color: white;"),
            Scripts(head),
            Body(root));
}