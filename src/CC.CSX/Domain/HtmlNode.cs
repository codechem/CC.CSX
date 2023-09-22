namespace CC.CSX;

using System.Collections;
using System.Text;

/// <summary>
/// represents a regular HTML element
/// if the name contains a #, it is split into the name and the id
/// </summary>
public class HtmlNode : HtmlItem, IEnumerable<HtmlNode>
{
    /// <summary>
    /// the optional id of the element
    /// </summary>
    public string? Id { get; set; }

    ///<summary>
    /// the children of the element
    /// </summary>
    public List<HtmlNode> Children { get; set; } = new();
    public List<HtmlAttribute> Attributes { get; set; } = new();

    public static implicit operator HtmlNode(string value)
        => new HtmlTextNode(value);

    public HtmlNode(string name,
            IEnumerable<HtmlAttribute>? attributes = null,
            IEnumerable<HtmlNode>? children = null) : base(name)
    {
        if (name.Contains("#"))
        {
            var parts = name.Split('#');
            Name = parts[0];
            Id = parts[1];
        }
        else
        {
            Name = name;
        }

        if (children is not null) Children = children.ToList();
        if (attributes is not null) Attributes = attributes.ToList();
    }

    public HtmlNode(string name, params HtmlItem[] children) : base(name)
    {
        Children = children.OfType<HtmlNode>().ToList();
        Attributes = children.OfType<HtmlAttribute>().ToList();
    }

    public HtmlNode(string name, IEnumerable<HtmlItem> children) : base(name)
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

    static void MaybeCr(StringBuilder sb)
    {
        if (RenderOptions.Indent > 0 && sb.Length > 0 && sb[sb.Length - 1] != '\n')
        {
            sb.AppendLine();
        }
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
        MaybeCr(sb);
        foreach (var child in Children)
        {
            sb.Append(child?.ToString(indent + RenderOptions.Indent));
            MaybeCr(sb);
        }
        MaybeCr(sb);
        sb.Append($"{new string(' ', indent)}</{Name}>");
        return sb.ToString();
    }

    public const char openTag = '<';
    public const char closeTag = '>';
    public const char space = ' ';
    public const char backslash = '/';
    public override void AppendTo(ref StringBuilder sb, int indent = 0){
        var indentStr = new string(' ', indent);
        sb.Append(indentStr).Append(openTag).Append(Name);
        if (Attributes.Any())
        {
            sb.Append(space);
            sb.Append(string.Join(space, Attributes));
        }
        sb.Append(closeTag);
        MaybeCr(sb);
        foreach (var child in Children)
        {
            child?.AppendTo(ref sb, indent + RenderOptions.Indent);
            MaybeCr(sb);
        }
        MaybeCr(sb);
        sb.Append(indentStr)
          .Append(openTag).Append(backslash)
          .Append(Name)
          .Append(closeTag);
    }

    public override string ToString() => ToString(0);

    public IEnumerator<HtmlNode> GetEnumerator() => this.When(x => true).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.When(x => true).GetEnumerator();
}