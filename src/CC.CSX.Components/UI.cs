namespace CC.CSX.Components;

using CC.CSX;

/// <summary>
/// Ready-to-use component factories. Import with <c>using static CC.CSX.Components.UI;</c>.
/// Each returns an <see cref="HtmlNode"/> styled by the ambient <see cref="Theme.Current"/>,
/// and accepts the usual mix of attributes and children — including extra <c>class</c>
/// attributes (merged) and htmx/hx-* attributes (passed through).
/// </summary>
public static class UI
{
    /// <summary>A button. Defaults to a neutral, medium button.</summary>
    public static HtmlNode Button(params HtmlItem[] items) => Button(Variant.Default, Size.Md, items);
    /// <summary>A button of the given <paramref name="variant"/>.</summary>
    public static HtmlNode Button(Variant variant, params HtmlItem[] items) => Button(variant, Size.Md, items);
    /// <summary>A button of the given <paramref name="variant"/> and <paramref name="size"/>.</summary>
    public static HtmlNode Button(Variant variant, Size size, params HtmlItem[] items)
        => ComponentBuilder.Build(HtmlElementKeys.button, Theme.Current.Button(variant, size), items);

    /// <summary>A small status/label badge.</summary>
    public static HtmlNode Badge(params HtmlItem[] items) => Badge(Variant.Default, Size.Md, items);
    /// <summary>A badge of the given <paramref name="variant"/>.</summary>
    public static HtmlNode Badge(Variant variant, params HtmlItem[] items) => Badge(variant, Size.Md, items);
    /// <summary>A badge of the given <paramref name="variant"/> and <paramref name="size"/>.</summary>
    public static HtmlNode Badge(Variant variant, Size size, params HtmlItem[] items)
        => ComponentBuilder.Build(HtmlElementKeys.span, Theme.Current.Badge(variant, size), items);

    /// <summary>An alert/callout. Defaults to the informational variant and sets <c>role="alert"</c>.</summary>
    public static HtmlNode Alert(params HtmlItem[] items) => Alert(Variant.Info, items);
    /// <summary>An alert/callout of the given <paramref name="variant"/>.</summary>
    public static HtmlNode Alert(Variant variant, params HtmlItem[] items)
    {
        var all = new HtmlItem[items.Length + 1];
        all[0] = ("role", "alert");
        Array.Copy(items, 0, all, 1, items.Length);
        return ComponentBuilder.Build(HtmlElementKeys.div, Theme.Current.Alert(variant), all);
    }

    /// <summary>The title element for use inside a <see cref="Card"/>.</summary>
    public static HtmlNode CardTitle(params HtmlItem[] items)
        => ComponentBuilder.Build(HtmlElementKeys.h2, Theme.Current.CardTitle(), items);

    /// <summary>
    /// A card. Items are placed inside the card body (use <see cref="CardTitle"/> for the heading).
    /// For full control over the outer element, compose <c>Div</c> + theme classes directly.
    /// </summary>
    public static HtmlNode Card(params HtmlItem[] items)
        => ComponentBuilder.Build(HtmlElementKeys.div, Theme.Current.Card(),
               ComponentBuilder.Build(HtmlElementKeys.div, Theme.Current.CardBody(), items));
}
