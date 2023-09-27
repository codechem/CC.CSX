namespace CC.CSX.Htmx;

using CC.CSX;

using static CC.CSX.HtmlElements;
public static partial class HtmxAttributes
{
    ///<summary>
    /// Creates a nunjucks for loop statement
    ///</summary>
    public static HtmlNode TmplFor(string condition, params HtmlNode[] children) => Fragment("{%for " + condition + " %}", Fragment(children), "{%endfor%}");

    /// <summary>
    /// Creates a htmx get attribute
    ///</summary>
    public static HtmlAttribute hxGet(string uri, string? swap = null, string? target = null, string? trigger = null)
    {
        var attrs = new List<HtmlAttribute>(){ hxGet(uri) };
        if (swap != null) attrs.Add(hxSwap(swap));
        if (target != null) attrs.Add(hxTarget(target));
        if (trigger != null) attrs.Add(hxTrigger(trigger));
        return new MultiHtmlAttribute(attributes: attrs);
    }

    ///<summary>
    /// Adds a script node for loading nunjucks 
    ///</summary>
    public static HtmlNode NunjucksImports = ScriptSrc("https://unpkg.com/nunjucks@3.2.3/browser/nunjucks.js");

    ///<summary>
    /// Adds a script node for loading htmx and hyperscript
    ///</summary>
    public static Fragment HtmxScriptImports = Fragment(
        ScriptSrc("https://unpkg.com/htmx.org@1.9.3"),
        ScriptSrc("https://unpkg.com/hyperscript.org@0.9.7"));
}