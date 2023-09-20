namespace CC.CSX.Htmx;

using CC.CSX;

using static CC.CSX.HtmlElements;
public static partial class HtmxAttributes
{
    public static HtmlNode TmplFor(string condition, params HtmlNode[] children) => Fragment("{%for " + condition + " %}", Fragment(children), "{%endfor%}");

    public static HtmlAttribute hxGet(string uri, string? swap = null, string? target = null, string? trigger = null)
    {
        var attrs = new List<HtmlAttribute>(){ hxGet(uri) };
        if (swap != null) attrs.Add(hxSwap(swap));
        if (target != null) attrs.Add(hxTarget(target));
        if (trigger != null) attrs.Add(hxTrigger(trigger));
        return new MultiHtmlAttribute(attributes: attrs);
    }
}