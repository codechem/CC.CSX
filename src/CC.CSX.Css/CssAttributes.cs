namespace CC.CSX.Css;

/// <summary>
/// Strongly typed counterparts of the string-based attribute factories.
/// Import with <c>using static CC.CSX.Css.CssAttributes;</c> next to
/// <c>using static CC.CSX.HtmlAttributes;</c> — the overloads do not collide,
/// so plain-string usage keeps working.
/// </summary>
public static class CssAttributes
{
    /// <summary>
    /// Builds an inline <c>style</c> attribute from typed declarations:
    /// <c>style(Display("flex"), Padding(8.px()))</c>.
    /// </summary>
    public static HtmlStyleAttribute style(params CssDeclaration[] declarations)
        => new(declarations.Select(d => (d.Property, d.Value)).ToArray());
}
