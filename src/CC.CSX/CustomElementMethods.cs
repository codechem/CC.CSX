namespace CC.CSX;
using Keys = CC.CSX.HtmlAttributeKeys;
public static partial class HtmlElements
{
    public static HtmlNode LinkRel(string rel = "stylesheet", params HtmlItem[] children) => Link(children).Add((Keys.rel, rel));
    public static HtmlNode LinkHref(string href, string rel = "stylesheet", params HtmlItem[] children) => Link(children).Add(new HtmlAttribute(Keys.rel, rel), new HtmlAttribute(Keys.href, href));
    public static HtmlNode ImgSrc(string src, params HtmlItem[] children) => Img(children).Add(new HtmlAttribute(Keys.src, src));
    public static HtmlNode AHref(string href, params HtmlItem[] children) => A(children).Add(new HtmlAttribute(Keys.href, href));
    public static HtmlNode ScriptSrc(string src, params HtmlItem[] children) => Script(children).Add(new HtmlAttribute(Keys.src, src));
    public static HtmlNone None => HtmlNone.Instance;
}