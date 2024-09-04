namespace CC.CSX.Htmx;

using CC.CSX;

using static CC.CSX.HtmlElements;
public static partial class HtmxAttributes
{
    ///<summary>
    /// Creates a nunjucks for loop statement
    ///</summary>
    public static HtmlNode TmplFor(string condition, params HtmlNode[] children) => Fragment("{%for " + condition + " %}", Fragment(children), "{%endfor%}");

    ///<summary>
    ///  issues a GET to the specified URL
    ///</summary>
    public static MultiHtmlAttribute hxGet(string url, string target = "this", string swap = "innerHTML", bool history = false, bool pushUrl = false, bool replaceUrl = false) => MultiAttr(
        hxGet(url),
        hxTarget(target),
        hxSwap(swap),
        hxHistory(history.ToString().ToLower()),
        hxPushUrl(pushUrl.ToString().ToLower()),
        hxReplaceUrl(replaceUrl.ToString().ToLower())
    );

    ///<summary>
    ///  issues a POST to the specified URL
    ///</summary>
    public static MultiHtmlAttribute hxPost(string url, string target = "this", string swap = "innerHTML", bool history = false, bool pushUrl = false, bool replaceUrl = false) => MultiAttr(
        hxPost(url),
        hxTarget(target),
        hxSwap(swap),
        hxHistory(history.ToString().ToLower()),
        hxPushUrl(pushUrl.ToString().ToLower()),
        hxReplaceUrl(replaceUrl.ToString().ToLower())
    );


    ///<summary>
    ///  issues a PUT to the specified URL
    ///</summary>
    public static MultiHtmlAttribute hxPut(string url, string target = "this", string swap = "innerHTML", bool history = false, bool pushUrl = false, bool replaceUrl = false) => MultiAttr(
        hxPut(url),
        hxTarget(target),
        hxSwap(swap),
        hxHistory(history.ToString().ToLower()),
        hxPushUrl(pushUrl.ToString().ToLower()),
        hxReplaceUrl(replaceUrl.ToString().ToLower())
    );

    ///<summary>
    ///  issues a PATCH to the specified URL
    ///</summary>
    public static MultiHtmlAttribute hxPatch(string url, string target = "this", string swap = "innerHTML", bool history = false, bool pushUrl = false, bool replaceUrl = false) => MultiAttr(
        hxPatch(url),
        hxTarget(target),
        hxSwap(swap),
        hxHistory(history.ToString().ToLower()),
        hxPushUrl(pushUrl.ToString().ToLower()),
        hxReplaceUrl(replaceUrl.ToString().ToLower())
    );

    ///<summary>
    ///  issues a DELETE to the specified URL
    ///</summary>
    public static MultiHtmlAttribute hxDelete(string url, string target = "this", string swap = "innerHTML", bool history = false, bool pushUrl = false, bool replaceUrl = false) => MultiAttr(
        hxDelete(url),
        hxTarget(target),
        hxSwap(swap),
        hxHistory(history.ToString().ToLower()),
        hxPushUrl(pushUrl.ToString().ToLower()),
        hxReplaceUrl(replaceUrl.ToString().ToLower())
    );

    ///<summary>
    /// Adds a script node for loading nunjucks 
    ///</summary>
    public static HtmlNode NunjucksImports = ScriptSrc("https://unpkg.com/nunjucks@3.2.3/browser/nunjucks.js");

    ///<summary>
    /// Adds a script node for loading htmx and hyperscript
    ///</summary>
    public static Fragment HtmxImports = Fragment(
        ScriptSrc("https://unpkg.com/htmx.org"),
        ScriptSrc("https://unpkg.com/hyperscript.org"));
}