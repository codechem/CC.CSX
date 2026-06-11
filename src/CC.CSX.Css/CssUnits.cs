using System.Globalization;

namespace CC.CSX.Css;

/// <summary>
/// Numeric-to-CSS-unit helpers: <c>8.px()</c>, <c>1.5.rem()</c>, <c>50.pct()</c>, …
/// </summary>
public static class CssUnits
{
    static string F(double value) => value.ToString(CultureInfo.InvariantCulture);

    /// <summary>Renders as <c>{value}px</c>.</summary>
    public static string px(this int value) => $"{value}px";
    /// <summary>Renders as <c>{value}px</c>.</summary>
    public static string px(this double value) => $"{F(value)}px";
    /// <summary>Renders as <c>{value}rem</c>.</summary>
    public static string rem(this int value) => $"{value}rem";
    /// <summary>Renders as <c>{value}rem</c>.</summary>
    public static string rem(this double value) => $"{F(value)}rem";
    /// <summary>Renders as <c>{value}em</c>.</summary>
    public static string em(this int value) => $"{value}em";
    /// <summary>Renders as <c>{value}em</c>.</summary>
    public static string em(this double value) => $"{F(value)}em";
    /// <summary>Renders as <c>{value}%</c>.</summary>
    public static string pct(this int value) => $"{value}%";
    /// <summary>Renders as <c>{value}%</c>.</summary>
    public static string pct(this double value) => $"{F(value)}%";
    /// <summary>Renders as <c>{value}vw</c>.</summary>
    public static string vw(this int value) => $"{value}vw";
    /// <summary>Renders as <c>{value}vw</c>.</summary>
    public static string vw(this double value) => $"{F(value)}vw";
    /// <summary>Renders as <c>{value}vh</c>.</summary>
    public static string vh(this int value) => $"{value}vh";
    /// <summary>Renders as <c>{value}vh</c>.</summary>
    public static string vh(this double value) => $"{F(value)}vh";
    /// <summary>Renders as <c>{value}fr</c> (grid fraction).</summary>
    public static string fr(this int value) => $"{value}fr";
    /// <summary>Renders as <c>{value}fr</c> (grid fraction).</summary>
    public static string fr(this double value) => $"{F(value)}fr";
    /// <summary>Renders as <c>{value}s</c> (seconds).</summary>
    public static string s(this double value) => $"{F(value)}s";
    /// <summary>Renders as <c>{value}ms</c> (milliseconds).</summary>
    public static string ms(this int value) => $"{value}ms";
}
