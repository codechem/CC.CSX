namespace CC.CSX;
using Keys = CC.CSX.HtmlAttributeKeys;
public static partial class HtmlElements
{

    /// <summary>
    /// A node that does not render itself, but just its children.
    /// </summary>
    public static Fragment Fragment(params HtmlNode[] children)
    {
        return new(children);
    }

    /// <summary>
    /// Creates new instance of <see cref="Link"/> by setting the <see cref="Keys.rel"/> attribute to "stylesheet".
    /// </summary>
    public static HtmlNode LinkRel(string rel = "stylesheet", params HtmlItem[] children)
    {
        return Link(children).Add((Keys.rel, rel));
    }

    /// <summary>
    /// Creates new instance of <see cref="Link"/> by setting the <see cref="Keys.rel"/> attribute to "stylesheet".
    /// </summary>
    public static HtmlNode LinkHref(string href, string rel = "stylesheet", params HtmlItem[] children)
    {
        return Link(children).Add(new HtmlAttribute(Keys.rel, rel), new HtmlAttribute(Keys.href, href));
    }

    /// <summary>
    /// Creates new instance of <see cref="Img"/> by setting the <see cref="Keys.src"/> attribute.
    /// </summary>
    public static HtmlNode ImgSrc(string src, params HtmlItem[] children)
    {
        return Img(children).Add(new HtmlAttribute(Keys.src, src));
    }

    /// <summary>
    /// Creates new instance of <see cref="A"/> by setting the <see cref="Keys.href"/> attribute.
    /// </summary>
    public static HtmlNode AHref(string href, params HtmlItem[] children)
    {
        return A(children).Add(new HtmlAttribute(Keys.href, href));
    }

    /// <summary>
    /// Creates new instance of <see cref="Script"/> by setting the <see cref="Keys.src"/> attribute.
    /// </summary>
    public static HtmlNode ScriptSrc(string src, params HtmlItem[] children)
    {
        return Script(children).Add(new HtmlAttribute(Keys.src, src));
    }

    /// <summary>
    /// Creates a new multi attribute with the given attributes.
    /// </summary>
    public static MultiHtmlAttribute MultiAttr(params HtmlAttribute[] attributes)
    {
        return new(attributes: attributes);
    }

    /// <summary>
    /// None node
    /// </summary>
    public static HtmlNone None => HtmlNone.Instance;

    /// <summary>
    /// A node that renders the given pre-rendered HTML verbatim (no escaping). Use only for trusted
    /// HTML; see <see cref="FragmentCache"/> for caching dynamically-built fragments.
    /// </summary>
    public static RawHtml Raw(string html) => new(html);

    /// <summary>
    /// Marks a dynamic hole: <paramref name="produce"/> is evaluated on each render. Surrounding
    /// static markup can be baked by <see cref="RenderPlan"/>; without a plan it renders inline.
    /// </summary>
    public static DynNode Dyn(Func<HtmlNode> produce) => new(produce);

    /// <summary>
    /// Marks a repeated dynamic region: renders <paramref name="render"/> for each of
    /// <paramref name="items"/>. Compiles to a loop hole in a <see cref="RenderPlan"/>.
    /// </summary>
    public static EachNode<T> Each<T>(IEnumerable<T> items, Func<T, HtmlNode> render) => new(items, render);


}