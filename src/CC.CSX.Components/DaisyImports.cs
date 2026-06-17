namespace CC.CSX.Components;

using CC.CSX;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;

/// <summary>
/// Helpers for getting daisyUI/Tailwind CSS onto the page. Drop the result into
/// <c>&lt;head&gt;</c>. The <see cref="Cdn"/> path is a zero-build convenience for development
/// and prototyping; for production you should compile Tailwind + daisyUI yourself (see remarks).
/// </summary>
/// <remarks>
/// <para><b>Production:</b> run a Tailwind build that loads the daisyUI plugin and emits a single
/// stylesheet, then serve it (e.g. via <see cref="CC.CSX.Css.CssImports"/> or a static file).
/// Because your class names live in C#, point Tailwind's content scan at your compiled output or
/// maintain a safelist; daisyUI's own component classes (<c>btn</c>, <c>card</c>, ...) are emitted
/// by the plugin regardless.</para>
/// <para>The default CDN URLs target daisyUI 5 and the Tailwind v4 browser build. They are not
/// pinned to a patch version and should be verified for your setup; override them via the
/// parameters if needed.</para>
/// </remarks>
public static class DaisyImports
{
    /// <summary>Default Tailwind v4 in-browser build (development only).</summary>
    public const string TailwindBrowserUrl = "https://cdn.jsdelivr.net/npm/@tailwindcss/browser@4";

    /// <summary>Default prebuilt daisyUI 5 stylesheet.</summary>
    public const string DaisyUiUrl = "https://cdn.jsdelivr.net/npm/daisyui@5";

    /// <summary>
    /// Development-only CDN tags: the Tailwind browser runtime plus the daisyUI stylesheet.
    /// Place inside <c>&lt;head&gt;</c>. Not for production (no purging, runtime compile cost).
    /// </summary>
    public static HtmlNode Cdn(string daisyUiUrl = DaisyUiUrl, string tailwindUrl = TailwindBrowserUrl) =>
        new Fragment(
            Script(src(tailwindUrl)),
            Link(rel("stylesheet"), href(daisyUiUrl), ("type", "text/css"))
        );

    /// <summary>A <c>&lt;link rel="stylesheet"&gt;</c> to a CSS file you built and serve yourself.</summary>
    public static HtmlNode StyleSheet(string url) =>
        Link(rel("stylesheet"), href(url), ("type", "text/css"));
}
