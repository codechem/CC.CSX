namespace CC.CSX;

/// <summary>
/// A class that contains options for html rendering.
/// </summary>
public static class RenderOptions
{
    /// <summary>
    /// The indentation to use when rendering html.
    /// If this is set to 0, then no indentation will be used and the html will be rendered in a single line.
    /// </summary>
    public static int Indent { get; set; } = 2;
}