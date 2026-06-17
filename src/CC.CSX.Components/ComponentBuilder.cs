namespace CC.CSX.Components;

using CC.CSX;
using CC.CSX.Css;

/// <summary>
/// Internal helper that assembles a component node, merging the theme's structural
/// classes with any caller-supplied <c>class</c> attribute into a single attribute
/// (HtmlNode does not dedupe attributes, so two <c>class</c> attributes would otherwise
/// both render). All other items (attributes, children, hx-* etc.) pass through untouched.
/// </summary>
static class ComponentBuilder
{
    public static HtmlNode Build(string tag, CssClass baseClass, params HtmlItem[] items)
    {
        var merged = baseClass;
        var rest = new List<HtmlItem>(items.Length);

        foreach (var item in items)
        {
            if (item is HtmlAttribute attr && attr.Name == HtmlAttributeKeys.@class)
            {
                if (!string.IsNullOrWhiteSpace(attr.Value))
                {
                    merged += new CssClass(attr.Value!);
                }
                continue;
            }
            rest.Add(item);
        }

        // pass the List as IEnumerable<HtmlItem> (avoids the array→span/IEnumerable ctor ambiguity)
        var node = new HtmlNode(tag, (IEnumerable<HtmlItem>)rest);
        if (!string.IsNullOrEmpty(merged.Name))
        {
            // class first so rendered markup reads naturally
            node.Attributes.Insert(0, new HtmlClassAttribute(merged.Name));
        }
        return node;
    }
}
