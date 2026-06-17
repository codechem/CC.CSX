namespace CC.CSX.Components;

using CC.CSX.Css;

/// <summary>
/// The styling backend for the component library. A theme maps semantic component
/// requests (a primary button, a success alert) to the <see cref="CssClass"/> that
/// realizes them. Swapping the active theme (see <see cref="Theme"/>) reskins every
/// component without touching call sites — this is also the seam a future non-web
/// (e.g. native) backend would implement.
/// </summary>
/// <remarks>
/// Every member returns a <see cref="CssClass"/>; returning <see cref="CssClass.None"/>
/// is valid and renders nothing. Components merge the returned class with any caller
/// supplied <c>class</c> attribute, so themes should return only the structural classes.
/// </remarks>
public interface IComponentTheme
{
    /// <summary>Classes for a button of the given <paramref name="variant"/> and <paramref name="size"/>.</summary>
    CssClass Button(Variant variant, Size size);

    /// <summary>Classes for a card container.</summary>
    CssClass Card();

    /// <summary>Classes for the inner body/padding region of a card.</summary>
    CssClass CardBody();

    /// <summary>Classes for the title element inside a card.</summary>
    CssClass CardTitle();

    /// <summary>Classes for an alert of the given <paramref name="variant"/>.</summary>
    CssClass Alert(Variant variant);

    /// <summary>Classes for a badge of the given <paramref name="variant"/> and <paramref name="size"/>.</summary>
    CssClass Badge(Variant variant, Size size);
}
