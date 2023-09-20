namespace CC.CSX;
using Keys = CC.CSX.HtmlAttributeKeys;
public static partial class HtmlElements
{
    public static HtmlNode LinkRel(string rel="stylesheet", params HtmlItem[] children) => Link(Concat(children, (Keys.rel, rel)));
    public static HtmlNode LinkHref(string href, string rel="stylesheet",  params HtmlItem[] children) => Link(Concat(children, (Keys.rel, rel), (Keys.href, href)));
    public static HtmlNode ImgSrc(string src, params HtmlItem[] children) => Img(Concat(children, (Keys.src, src)));
    public static HtmlNode AHref(string href, params HtmlItem[] children) => A(Concat(children, (Keys.href, href)));
    public static HtmlNode ScriptSrc(string src, params HtmlItem[] children) => Script(Concat(children, (Keys.src, src)));
    public static HtmlNone None => HtmlNone.Instance;
    public static T[] Concat<T>(params IEnumerable<T>[] children) => (children.SelectMany(x => x).ToArray());
    public static T[] Concat<T>(IEnumerable<T> main, params T[] children) => children.Concat(main).ToArray();
}

public class HtmlNone : HtmlItem
{
    public static readonly HtmlNone Instance = new HtmlNone();
    private HtmlNone():base("None"){}
    public override string ToString() => "";
    public override string ToString(int indent = 0)=> "";
}