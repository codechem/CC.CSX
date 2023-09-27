namespace CC.CSX;
using System.Text;
using System.Text.Json.Serialization;

/// <summary>
/// An abstract class that represents a node or a node attribute, that can be rendered to HTML.
/// </summary>
public abstract class HtmlItem
{
    ///<summary>
    /// The name of the node or attribute.
    ///</summary>
    public string Name { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Value { get; set; }
    /// <summary>
    /// Creates a new instance of <see cref="HtmlItem"/> with the given name.
    /// </summary>
    public HtmlItem(in string name) => Name = name;
    /// <summary>
    /// Creates a new instance of <see cref="HtmlItem"/> with the given name and value.
    /// </summary>
    public HtmlItem(in string name, string? value)
    {
        Name = name;
        Value = value;
    }
    /// <summary>
    /// Implicit conversion from <see cref="string"/> to <see cref="HtmlItem"/>.
    /// </summary>
    public static implicit operator HtmlItem(string value) => new HtmlTextNode(value);
    public static implicit operator HtmlItem((string key, string? value) tuple) => new HtmlAttribute(in tuple.key, tuple.value);

    /// <summary>
    /// Renders the item to HTML by taking into account the indentation.
    /// </summary>
    public abstract string ToString(int indent = 0);
    public abstract void AppendTo(ref StringBuilder sb, int indent = 0);
}