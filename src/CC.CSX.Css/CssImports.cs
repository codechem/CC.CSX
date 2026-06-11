using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;

namespace CC.CSX.Css;

/// <summary>
/// Helpers for getting <see cref="ICssBundle"/> content onto a page,
/// in the spirit of <c>HtmxImports</c>.
/// </summary>
public static class CssImports
{
    /// <summary>Emits one inline <c>&lt;style&gt;</c> element per bundle. Place inside <c>Head(...)</c>.</summary>
    public static HtmlNode Inline(params ICssBundle[] bundles)
        => new Fragment(bundles.Select(b => Style(new HtmlTextNode(b.Content))));

    /// <summary>Emits a <c>&lt;link rel="stylesheet"&gt;</c> for an external stylesheet.</summary>
    public static HtmlNode StyleSheet(string url)
        => Link(rel("stylesheet"), href(url));

    /// <summary>
    /// Emits a cache-busted <c>&lt;link rel="stylesheet"&gt;</c> for a bundle served at
    /// <c>{basePath}/{Name}.css?v={ContentHash}</c>.
    /// </summary>
    public static HtmlNode StyleSheet(ICssBundle bundle, string basePath = "/css")
        => StyleSheet($"{basePath}/{bundle.Name}.css?v={bundle.ContentHash}");
}
