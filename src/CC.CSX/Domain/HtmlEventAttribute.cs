namespace CC.CSX;

///<summary>
/// Represents an HTML event attribute.
/// It is a <see cref="HtmlAttribute"/> that has a name and a value but the value is a function.
///</summary>
public class HtmlEventAttribute : HtmlAttribute
{
    /// <summary>
    /// Creates a new instance of <see cref="HtmlEventAttribute"/>
    /// </summary>
    public HtmlEventAttribute(string name) : base(name)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="HtmlEventAttribute"/>
    /// </summary>
    public HtmlEventAttribute(string name, string? value) : base(name, value)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="HtmlEventAttribute"/>
    /// </summary>
    public HtmlEventAttribute(string name, Action<EventArgs> handler) : base(name)
    {
    }
}