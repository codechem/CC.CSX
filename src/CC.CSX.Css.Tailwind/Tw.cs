namespace CC.CSX.Css.Tailwind;

using CC.CSX.Css;

/// <summary>
/// Strongly typed Tailwind CSS utilities. The constants (in <c>Tw.Classes.g.cs</c>, generated
/// by <c>eng/gen-tailwind-classes.py</c>) cover the default theme; the methods here add
/// variant prefixes (<c>hover(...)</c>, <c>md(...)</c>, …) and arbitrary values.
/// Everything returns a <see cref="CssClass"/>, so utilities compose with <c>+</c> into
/// virtual classes and drop into <c>@class(...)</c> next to plain strings.
/// <code>
/// using static CC.CSX.Css.Tailwind.Tw;
/// var card = bgWhite + roundedLg + shadowMd + p4 + hover(shadowLg) + dark(bgSlate800);
/// Div(@class(card, "my-extra-class"), ...)
/// </code>
/// </summary>
public static partial class Tw
{
    /// <summary>Applies an arbitrary Tailwind variant prefix to every token: <c>variant("aria-busy", p4)</c> → <c>aria-busy:p-4</c>.</summary>
    public static CssClass variant(string prefix, CssClass cssClass)
        => new(string.Join(' ', cssClass.Tokens.Select(t => $"{prefix}:{t}")));

    /// <summary>An arbitrary-value utility: <c>arbitrary("mt", "17px")</c> → <c>mt-[17px]</c>.</summary>
    public static CssClass arbitrary(string utility, string value)
        => new($"{utility}-[{value}]");

    /// <summary>Negates every token: <c>neg(mt4)</c> → <c>-mt-4</c>.</summary>
    public static CssClass neg(CssClass cssClass)
        => new(string.Join(' ', cssClass.Tokens.Select(t => $"-{t}")));

    /// <summary>Marks every token important: <c>important(mt4)</c> → <c>!mt-4</c>.</summary>
    public static CssClass important(CssClass cssClass)
        => new(string.Join(' ', cssClass.Tokens.Select(t => $"!{t}")));

    // state variants
    /// <summary><c>hover:</c> variant.</summary>
    public static CssClass hover(CssClass c) => variant("hover", c);
    /// <summary><c>focus:</c> variant.</summary>
    public static CssClass focus(CssClass c) => variant("focus", c);
    /// <summary><c>focus-within:</c> variant.</summary>
    public static CssClass focusWithin(CssClass c) => variant("focus-within", c);
    /// <summary><c>focus-visible:</c> variant.</summary>
    public static CssClass focusVisible(CssClass c) => variant("focus-visible", c);
    /// <summary><c>active:</c> variant.</summary>
    public static CssClass active(CssClass c) => variant("active", c);
    /// <summary><c>visited:</c> variant.</summary>
    public static CssClass visited(CssClass c) => variant("visited", c);
    /// <summary><c>disabled:</c> variant.</summary>
    public static CssClass disabled(CssClass c) => variant("disabled", c);
    /// <summary><c>first:</c> variant.</summary>
    public static CssClass first(CssClass c) => variant("first", c);
    /// <summary><c>last:</c> variant.</summary>
    public static CssClass last(CssClass c) => variant("last", c);
    /// <summary><c>odd:</c> variant.</summary>
    public static CssClass odd(CssClass c) => variant("odd", c);
    /// <summary><c>even:</c> variant.</summary>
    public static CssClass even(CssClass c) => variant("even", c);
    /// <summary><c>group-hover:</c> variant.</summary>
    public static CssClass groupHover(CssClass c) => variant("group-hover", c);
    /// <summary><c>peer-hover:</c> variant.</summary>
    public static CssClass peerHover(CssClass c) => variant("peer-hover", c);

    // theme variants
    /// <summary><c>dark:</c> variant.</summary>
    public static CssClass dark(CssClass c) => variant("dark", c);
    /// <summary><c>motion-reduce:</c> variant.</summary>
    public static CssClass motionReduce(CssClass c) => variant("motion-reduce", c);

    // responsive variants (mobile-first breakpoints)
    /// <summary><c>sm:</c> breakpoint (≥640px).</summary>
    public static CssClass sm(CssClass c) => variant("sm", c);
    /// <summary><c>md:</c> breakpoint (≥768px).</summary>
    public static CssClass md(CssClass c) => variant("md", c);
    /// <summary><c>lg:</c> breakpoint (≥1024px).</summary>
    public static CssClass lg(CssClass c) => variant("lg", c);
    /// <summary><c>xl:</c> breakpoint (≥1280px).</summary>
    public static CssClass xl(CssClass c) => variant("xl", c);
    /// <summary><c>2xl:</c> breakpoint (≥1536px).</summary>
    public static CssClass xl2(CssClass c) => variant("2xl", c);
}
