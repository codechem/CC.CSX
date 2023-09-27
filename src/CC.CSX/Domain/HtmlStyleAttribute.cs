namespace CC.CSX;

/// <summary>
/// A style attribute that takes a list of attributes and joins them with a semicolon.
/// </summary>
public class HtmlStyleAttribute : HtmlAttribute
{
    /// <inheritdoc/>
    public HtmlStyleAttribute(string value) : base("style", value) { }

    /// <inheritdoc/>
    public HtmlStyleAttribute(params (string key, string value)[] attributes) : base("style", string.Join(";", attributes.Select(x => $"{x.key}:{x.value}"))) { }
}