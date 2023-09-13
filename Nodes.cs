using System.Text;
namespace CC.CSX;
public static class RenderOptions
{
    public static int Indent { get; set; } = 2;
}

public class HtmlItem
{
    public string Name { get; set; }
    public string? Value { get; set; }
    public HtmlItem(string name) => Name = name;
    public HtmlItem(string name, string? value)
    {
        Name = name;
        Value = value;
    }

    public static implicit operator HtmlItem(string value) => new TextNode(value);
    public static implicit operator HtmlItem((string key, string? value) tuple) => new HtmlAttribute(tuple.key, tuple.value);
}

public class Shallow : HtmlNode
{
    public Shallow(params HtmlItem[] children) : base("shallow", children) { }
    public override string ToString(int indent = 0)
    {
        return string.Join("", Children.Select(x => x.ToString(indent)));
    }
}

public class HtmlAttribute : HtmlItem
{
    public HtmlAttribute(string name, string? value) : base(name, value) { }
    public HtmlAttribute(string name) : base(name) { }
    public override string ToString()
    {
        if(Value is null)
            return Name;
        return $"{Name}=\"{Value}\"";
    }

    public static implicit operator HtmlAttribute((string key, string value) tuple)
    {
        return new HtmlAttribute(tuple.key, tuple.value);
    }
}

public class StyleAttribute : HtmlAttribute
{
    public StyleAttribute(string value) : base("style", value) { }
    public StyleAttribute(params HtmlAttribute[] attributes) : base("style", string.Join(";", attributes.Select(x => x.ToString()))) { }
}

public class TextNode : HtmlNode
{
    public TextNode(string value) : base("#text", value) { }

    public override string ToString()
    {
        return Value ?? "";
    }
    public override string ToString(int indent = 0)
    {
        return (new string(' ', indent) + Value) ?? "";
    }
}

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

    public virtual string ToString(int indent = 0)
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
    public override string ToString()
    {
        return ToString(0);
    }
}