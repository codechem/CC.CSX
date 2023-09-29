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

    /// <summary>
    /// The value of the item.
    /// If the item is an attribute, this is the attribute value.
    /// If the item is a node, this is the inner text of the node.
    /// </summary>
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

    /// <summary>
    /// Implicit conversion from string tuple to <see cref="HtmlAttribute"/>.
    /// </summary>
    public static implicit operator HtmlItem((string key, string? value) tuple) => new HtmlAttribute(in tuple.key, tuple.value);

    /// <summary>
    /// Renders the item to HTML by taking into account the indentation.
    /// </summary>
    public abstract string ToString(int indent = 0);

    /// <summary>
    /// Appends the item to the <see cref="StringBuilder"/> by taking into account the indentation.
    /// </summary>
    public abstract void AppendTo(ref StringBuilder sb, int indent = 0);


    /// <summary>
    /// Writes the item to the <see cref="TextWriter"/> by taking into account the indentation.
    /// </summary>
    public abstract void WriteTo(ref TextWriter tw, int indent = 0);
}