namespace CC.CSX.Components;

/// <summary>
/// Holds the ambient <see cref="IComponentTheme"/> used by the component factories.
/// Defaults to <see cref="DaisyUiTheme"/>. Assign <see cref="Current"/> once at startup
/// to reskin the whole app, or use <see cref="With"/> to scope a theme to a block.
/// </summary>
public static class Theme
{
    static readonly AsyncLocal<IComponentTheme?> Override = new();
    static IComponentTheme _default = new DaisyUiTheme();

    /// <summary>
    /// The active theme. Reads return the scoped override (see <see cref="With"/>) when one
    /// is in effect, otherwise the process-wide default. Setting this replaces the default.
    /// </summary>
    public static IComponentTheme Current
    {
        get => Override.Value ?? _default;
        set => _default = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Runs <paramref name="body"/> with <paramref name="theme"/> as the active theme,
    /// restoring the previous one afterwards. Useful for previewing or per-request themes.
    /// </summary>
    public static T With<T>(IComponentTheme theme, Func<T> body)
    {
        var prev = Override.Value;
        Override.Value = theme ?? throw new ArgumentNullException(nameof(theme));
        try { return body(); }
        finally { Override.Value = prev; }
    }
}
