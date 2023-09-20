namespace CC.CSX;
using System.Text.Json.Serialization;

/// <summary>
/// An abstract class that represents a node or a node attribute, that can be rendered to HTML.
/// </summary>
public abstract class HtmlItem
{
    ///<summary>
    /// The name of the node or attribute.
    ///</summary>
    public string Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Value { get; set; }
    public HtmlItem(string name) => Name = name;
    public HtmlItem(string name, string? value)
    {
        Name = name;
        Value = value;
    }
    public static implicit operator HtmlItem(string value) => new HtmlTextNode(value);
    public static implicit operator HtmlItem((string key, string? value) tuple) => new HtmlAttribute(tuple.key, tuple.value);
    /// <summary>
    /// Renders the item to HTML by taking into account the indentation.
    /// </summary>
    public abstract string ToString(int indent = 0);
}