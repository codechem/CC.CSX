namespace CC.CSX;
using Keys = CC.CSX.HtmlAttributeKeys;
public static partial class HtmlElements
{

    /// <summary>
    /// A node that does not render itself, but just its children.
    /// </summary>
    public static Fragment Fragment(params HtmlNode[] children) => new Fragment(children);
    /// <summary>
    /// Creates new instance of <see cref="Link"/> by setting the <see cref="Keys.rel"/> attribute to "stylesheet".
    /// </summary>
    public static HtmlNode LinkRel(string rel = "stylesheet", params HtmlItem[] children) => Link(children).Add((Keys.rel, rel));
    /// <summary>
    /// Creates new instance of <see cref="Link"/> by setting the <see cref="Keys.rel"/> attribute to "stylesheet".
    /// </summary>
    public static HtmlNode LinkHref(string href, string rel = "stylesheet", params HtmlItem[] children) => Link(children).Add(new HtmlAttribute(Keys.rel, rel), new HtmlAttribute(Keys.href, href));
    /// <summary>
    /// Creates new instance of <see cref="Img"/> by setting the <see cref="Keys.src"/> attribute.
    /// </summary>
    public static HtmlNode ImgSrc(string src, params HtmlItem[] children) => Img(children).Add(new HtmlAttribute(Keys.src, src));
    /// <summary>
    /// Creates new instance of <see cref="A"/> by setting the <see cref="Keys.href"/> attribute.
    /// </summary>
    public static HtmlNode AHref(string href, params HtmlItem[] children) => A(children).Add(new HtmlAttribute(Keys.href, href));
    /// <summary>
    /// Creates new instance of <see cref="Script"/> by setting the <see cref="Keys.src"/> attribute.
    /// </summary>
    public static HtmlNode ScriptSrc(string src, params HtmlItem[] children) => Script(children).Add(new HtmlAttribute(Keys.src, src));
    /// <summary>
    /// Creates a new multi attribute with the given attributes.
    /// </summary>
    public static HtmlAttribute MultiAttr(params HtmlAttribute[] attributes) => new MultiHtmlAttribute(attributes:attributes);
    /// <summary>
    /// None node
    /// </summary>
    public static HtmlNone None => HtmlNone.Instance;


}