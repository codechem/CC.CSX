namespace CC.CSX;
using System.Text;
using System.Text.Json.Serialization;

/// <summary>
/// represents a regular HTML element
/// if the name contains a #, it is split into the name and the id
/// </summary>
public class HtmlNode : HtmlItem
{
    /// <summary>
    /// the optional id of the element
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(0)]
    public string? Id { get; set; }


    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(1)]
    public List<HtmlAttribute> Attributes { get; set; } = new();
    
    ///<summary>
    /// the children of the element
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(2)]
    public List<HtmlNode> Children { get; set; } = new();

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

    static string MaybeCr(string sb)
    {
        if (RenderOptions.Indent > 0 && sb.Length > 0 && sb[sb.Length - 1] != '\n')
        {
            return sb + "\n";
        }
        return sb;
    }

    public string ToStringNaive(int indent = 0)
    {
        var sb = "";
        sb+=$"{new string(' ', indent)}<{Name}";
        if (Attributes.Any())
        {
            sb+=" ";
            sb+=string.Join(" ", Attributes);
        }
        sb+=">";
        MaybeCr(sb);
        foreach (var child in Children)
        {
            sb+=child?.ToStringNaive(indent + RenderOptions.Indent);
            MaybeCr(sb);
        }
        MaybeCr(sb);
        sb+=$"{new string(' ', indent)}</{Name}>";
        return sb.ToString();
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

    const char openTag = '<';
    const char closeTag = '>';
    const char space = ' ';
    const char backslash = '/';

    public override void AppendTo(ref StringBuilder sb, int indent = 0)
    {
        var sw = new StringWriter(sb) as TextWriter;
        WriteTo(ref sw, indent);
    }

    public virtual void WriteTo(ref TextWriter tw, int indent = 0)
    {
        var indentStr = new string(' ', indent);
        tw.Write(indentStr);
        tw.Write(openTag);
        tw.Write(Name);
        foreach (var attr in Attributes)
        {
            tw.Write(space);
            attr.WriteTo(ref tw);
        }
        tw.Write(closeTag);
        bool newLines = Children.Count > 0 && RenderOptions.Indent > 0;
        if (newLines) tw.WriteLine();
        foreach (var child in Children)
        {
            child?.WriteTo(ref tw, indent + RenderOptions.Indent);
            if (newLines) tw.WriteLine();
        }
        tw.Write(indentStr);
        tw.Write(openTag);
        tw.Write(backslash);
        tw.Write(Name);
        tw.Write(closeTag);
    }

    public override string ToString() => ToString(0);

    // public IEnumerator<HtmlNode> GetEnumerator() => this.When(x => true).GetEnumerator();
    //
    // IEnumerator IEnumerable.GetEnumerator() => this.When(x => true).GetEnumerator();
}