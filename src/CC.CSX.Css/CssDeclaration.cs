namespace CC.CSX.Css;

/// <summary>
/// A strongly typed CSS declaration (<c>property: value</c> pair) for inline styles.
/// Converts implicitly to the <c>(string, string)</c> tuple form that
/// <see cref="HtmlStyleAttribute"/> already understands.
/// </summary>
/// <code>
/// using static CC.CSX.Css.CssProperties;
/// Div(style(Display("flex"), Padding(8.px()), Color("#336")), ...)
/// </code>
public readonly record struct CssDeclaration(string Property, string Value)
{
    /// <summary>Usable wherever a <c>(key, value)</c> style tuple is expected.</summary>
    public static implicit operator (string key, string value)(CssDeclaration declaration)
        => (declaration.Property, declaration.Value);

    /// <summary>Plain tuples keep working wherever a <see cref="CssDeclaration"/> is expected.</summary>
    public static implicit operator CssDeclaration((string key, string value) tuple)
        => new(tuple.key, tuple.value);

    /// <summary>Renders as <c>property:value</c>.</summary>
    public override string ToString() => $"{Property}:{Value}";
}
