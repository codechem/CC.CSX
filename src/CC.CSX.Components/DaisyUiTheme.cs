namespace CC.CSX.Components;

using CC.CSX.Css;

/// <summary>
/// Default <see cref="IComponentTheme"/> that renders daisyUI markup. daisyUI is itself
/// themeable at runtime via <c>data-theme</c> on a parent element (e.g. <c>&lt;html data-theme="dark"&gt;</c>)
/// and via CSS custom properties (<c>--color-primary</c>, ...), so most restyling needs no C# change.
/// Subclass and override individual members to tweak a single component family.
/// </summary>
public class DaisyUiTheme : IComponentTheme
{
    // Class names are returned as literals (not interpolated) on purpose: a Tailwind/daisyUI
    // build can only keep classes it can find as literal text. The full set this theme can
    // emit is also published as a safelist — see CC.CSX.Components/daisyui.safelist.css.

    /// <inheritdoc/>
    public virtual CssClass Button(Variant variant, Size size)
    {
        CssClass color = variant switch
        {
            Variant.Primary => "btn-primary",
            Variant.Secondary => "btn-secondary",
            Variant.Accent => "btn-accent",
            Variant.Info => "btn-info",
            Variant.Success => "btn-success",
            Variant.Warning => "btn-warning",
            Variant.Error => "btn-error",
            Variant.Ghost => "btn-ghost",
            _ => CssClass.None,
        };
        CssClass sz = size switch
        {
            Size.Xs => "btn-xs",
            Size.Sm => "btn-sm",
            Size.Lg => "btn-lg",
            _ => CssClass.None,
        };
        return new CssClass("btn") + color + sz;
    }

    /// <inheritdoc/>
    public virtual CssClass Card() => new CssClass("card") + "bg-base-100" + "shadow-md";

    /// <inheritdoc/>
    public virtual CssClass CardBody() => "card-body";

    /// <inheritdoc/>
    public virtual CssClass CardTitle() => "card-title";

    /// <inheritdoc/>
    public virtual CssClass Alert(Variant variant)
    {
        CssClass color = variant switch
        {
            Variant.Info => "alert-info",
            Variant.Success => "alert-success",
            Variant.Warning => "alert-warning",
            Variant.Error => "alert-error",
            _ => CssClass.None,
        };
        return new CssClass("alert") + color;
    }

    /// <inheritdoc/>
    public virtual CssClass Badge(Variant variant, Size size)
    {
        CssClass color = variant switch
        {
            Variant.Primary => "badge-primary",
            Variant.Secondary => "badge-secondary",
            Variant.Accent => "badge-accent",
            Variant.Info => "badge-info",
            Variant.Success => "badge-success",
            Variant.Warning => "badge-warning",
            Variant.Error => "badge-error",
            Variant.Ghost => "badge-ghost",
            _ => CssClass.None,
        };
        CssClass sz = size switch
        {
            Size.Xs => "badge-xs",
            Size.Sm => "badge-sm",
            Size.Lg => "badge-lg",
            _ => CssClass.None,
        };
        return new CssClass("badge") + color + sz;
    }
}
