namespace CC.CSX;

/// <summary>
/// A class attribute that takes a list of classes and joins them with a space.
/// </summary>
public class HtmlClassAttribute : HtmlAttribute
{
    public HtmlClassAttribute(string value) : base("class", value) { }
    public HtmlClassAttribute(params string[] classes) : base("class", string.Join(" ", classes)) { }
}
