using System.Text;
namespace CC.CSX;

/// <summary>
/// An abstract class that represents a node or a node attribute, that can be rendered to HTML.
/// </summary>
public abstract class HtmlItem
{
    public string Name { get; set; }
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


/// <summary>
/// represents a regular HTML element
/// </summary>
public class HtmlNode : HtmlItem
{
    public List<HtmlNode> Children { get; set; } = new();
    public List<HtmlAttribute> Attributes { get; set; } = new();
    public HtmlNode(string name, params HtmlItem[] children) : base(name)
    {
        Children = children.OfType<HtmlNode>().ToList();
        Attributes = children.OfType<HtmlAttribute>().ToList();
    }

    public HtmlNode(string name, string value) : base(name, value) { }

    public HtmlNode Add(params HtmlItem[] children)
    {
        Children.AddRange(children.OfType<HtmlNode>());
        Attributes.AddRange(children.OfType<HtmlAttribute>());
        return this;
    }

    public override string ToString(int indent = 0)
    {
        var sb = new StringBuilder();
        sb.Append($"{new string(' ', indent)}<{Name}");
        if (Attributes.Any())
        {
            sb.Append(" ");
            sb.Append(string.Join(" ", Attributes));
        }
        sb.Append(">");
        foreach (var child in Children)
        {
            if (RenderOptions.Indent > 0)
                sb.AppendLine();
            sb.Append(child?.ToString(indent + RenderOptions.Indent));
        }

        if (RenderOptions.Indent > 0)
            sb.AppendLine();
        sb.Append($"{new string(' ', indent)}</{Name}>");
        return sb.ToString();
    }

    public override string ToString() => ToString(0);
}
public class HtmlAttribute : HtmlItem
{
    public HtmlAttribute(string name, string? value) : base(name, value) { }
    public HtmlAttribute(string name) : base(name) { }
    public override string ToString(int indent = 0) => Value is null ? Name : $"{Name}=\"{Value}\"";
    public override string ToString() => ToString(0);

    public static implicit operator HtmlAttribute((string key, string value) tuple) => new HtmlAttribute(tuple.key, tuple.value);
}

/// <summary>
/// A node that does not render itself, but just its children.
/// </summary>
public class ShallowNode : HtmlNode
{
    public ShallowNode(params HtmlItem[] children) : base("shallow", children) { }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <remarks>
    /// Because Shallow does not render itself, it does not take into account the indentation, and just passes it to its children.
    /// </remarks>
    public override string ToString(int indent = 0) => string.Join("", Children.Select(x => x.ToString(indent)));
}


/// <summary>
/// A node that renders itself as a regular text
/// </summary>
public class HtmlTextNode : HtmlNode
{
    public HtmlTextNode(string value) : base("#text", value) { }

    public override string ToString() => Value ?? "";
    ///<summary>
    ///<inheritdoc/>
    ///<summary>
    public override string ToString(int indent = 0) => (new string(' ', indent) + Value) ?? "";
}


/// <summary>
/// A style attribute that takes a list of attributes and joins them with a semicolon.
/// </summary>
public class HtmlStyleAttribute : HtmlAttribute
{
    public HtmlStyleAttribute(string value) : base("style", value) { }
    public HtmlStyleAttribute(params HtmlAttribute[] attributes) : base("style", string.Join(";", attributes.Select(x => x.ToString()))) { }
    public HtmlStyleAttribute(params (string key, string value)[] attributes) : base("style", string.Join(";", attributes.Select(x => $"{x.key}:{x.value}"))) { }
}

/// <summary>
/// A class attribute that takes a list of classes and joins them with a space.
/// </summary>
public class HtmlClassAttribute : HtmlAttribute
{
    public HtmlClassAttribute(string value) : base("class", value) { }
    public HtmlClassAttribute(params string[] classes) : base("class", string.Join(" ", classes)) { }
}


public static class HtmlNodeExtensions{
    public static void Apply(this HtmlNode node, Action<HtmlNode> action){
        action(node);
        foreach(var child in node.Children){
            child.Apply(action);
        }
    }
}