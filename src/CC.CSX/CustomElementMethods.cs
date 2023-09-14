namespace CC.CSX;
using Keys = CC.CSX.HtmlAttributeKeys;
public static partial class HtmlElements
{
    public static HtmlNode A(string href, params HtmlItem[] children) => A(Concat(children, (Keys.href, href)));
    public static HtmlNode Img(string src, params HtmlItem[] children) => Img(Concat(children, (Keys.src, src)));
    public static HtmlNode Link(string rel="stylesheet", params HtmlItem[] children) => Link(Concat(children, (Keys.rel, rel)));
    public static HtmlNode Link(string href, string rel="stylesheet",  params HtmlItem[] children) => Link(Concat(children, (Keys.rel, rel), (Keys.href, href)));
    public static HtmlNode ScriptSrc(string src, params HtmlItem[] children) => Script(Concat(children, (Keys.src, src)));

    public static T[] Concat<T>(params IEnumerable<T>[] children) => (children.SelectMany(x => x).ToArray());
    public static T[] Concat<T>(IEnumerable<T> main, params T[] children) => children.Concat(main).ToArray();
}