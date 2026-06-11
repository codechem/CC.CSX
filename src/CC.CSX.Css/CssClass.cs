namespace CC.CSX.Css;

/// <summary>
/// A strongly typed CSS class reference. The <see cref="Name"/> holds one or more
/// space-separated class tokens, so a <see cref="CssClass"/> can represent either a single
/// class or a <em>virtual (higher-order) class</em> — a named composition of several classes
/// that expands to a single class string when rendered.
/// </summary>
/// <remarks>
/// <para>It converts implicitly to <see cref="string"/>, so it can be passed directly to
/// <c>@class(...)</c> alongside plain strings, and to <see cref="HtmlAttribute"/>, so it can be
/// placed directly inside an element.</para>
/// <code>
/// static readonly CssClass Card = new("card");
/// static readonly CssClass Highlighted = new("card--highlighted");
/// // a virtual class composed of other classes:
/// static readonly CssClass FeaturedCard = Card + Highlighted + "shadow";
/// Div(@class(FeaturedCard, "mt-2"), ...)
/// </code>
/// </remarks>
public readonly record struct CssClass(string Name)
{
    /// <summary>A class that renders as nothing. Useful with ternary operators.</summary>
    public static readonly CssClass None = new(string.Empty);

    /// <summary>The individual class tokens this instance expands to.</summary>
    public string[] Tokens => string.IsNullOrWhiteSpace(Name)
        ? []
        : Name.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    /// <summary>
    /// Composes many classes into a single virtual class. Duplicate tokens are removed,
    /// first occurrence wins, order is preserved.
    /// </summary>
    public static CssClass Compose(params CssClass[] classes)
    {
        var seen = new HashSet<string>(StringComparer.Ordinal);
        var tokens = new List<string>();
        foreach (var cssClass in classes)
            foreach (var token in cssClass.Tokens)
                if (seen.Add(token))
                    tokens.Add(token);
        return new(string.Join(' ', tokens));
    }

    /// <summary>Combines two classes into a virtual class (duplicates removed).</summary>
    public static CssClass operator +(CssClass left, CssClass right) => Compose(left, right);

    /// <summary>A <see cref="CssClass"/> is usable wherever a class string is expected.</summary>
    public static implicit operator string(CssClass cssClass) => cssClass.Name ?? string.Empty;

    /// <summary>Plain strings keep working wherever a <see cref="CssClass"/> is expected.</summary>
    public static implicit operator CssClass(string name) => new(name ?? string.Empty);

    /// <summary>An array of classes expands to a single virtual class.</summary>
    public static implicit operator CssClass(CssClass[] classes) => Compose(classes);

    /// <summary>A <see cref="CssClass"/> used in element position becomes a <c>class</c> attribute.</summary>
    public static implicit operator HtmlAttribute(CssClass cssClass) => new HtmlClassAttribute(cssClass.Name ?? string.Empty);

    /// <summary>Returns the class string this instance expands to.</summary>
    public override string ToString() => Name ?? string.Empty;
}
