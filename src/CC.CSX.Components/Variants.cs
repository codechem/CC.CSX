namespace CC.CSX.Components;

/// <summary>Semantic intent of a component. Themes map each value to their own classes.</summary>
public enum Variant
{
    /// <summary>Neutral / default styling.</summary>
    Default,
    /// <summary>Primary brand action.</summary>
    Primary,
    /// <summary>Secondary action.</summary>
    Secondary,
    /// <summary>Accent / tertiary emphasis.</summary>
    Accent,
    /// <summary>Informational.</summary>
    Info,
    /// <summary>Positive / success.</summary>
    Success,
    /// <summary>Caution.</summary>
    Warning,
    /// <summary>Destructive / error.</summary>
    Error,
    /// <summary>Low-emphasis, borderless.</summary>
    Ghost,
}

/// <summary>Relative sizing of a component. Themes map each value to their own classes.</summary>
public enum Size
{
    /// <summary>Extra small.</summary>
    Xs,
    /// <summary>Small.</summary>
    Sm,
    /// <summary>Medium (default).</summary>
    Md,
    /// <summary>Large.</summary>
    Lg,
}
