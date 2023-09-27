namespace CC.CSX;

/// <summary>
/// A class attribute that takes a list of classes and joins them with a space.
/// </summary>
public class HtmlClassAttribute : HtmlAttribute
{
    /// <summary>
    /// Creates a new instance of <see cref="HtmlClassAttribute"/> with the given value.
    /// </summary>
    public HtmlClassAttribute(string value) : base("class", value) { }

    /// <summary>
    /// Creates a new instance of <see cref="HtmlClassAttribute"/> having one more more items in it.
    /// </summary>
    public HtmlClassAttribute(params string[] classes) : base("class", string.Join(" ", classes)) { }
}