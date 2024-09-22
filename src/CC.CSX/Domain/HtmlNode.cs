using System.Text;
using System.Text.Json.Serialization;

namespace CC.CSX;

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

    /// <summary>
    /// The element's attributes
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(1)]
    public List<HtmlAttribute> Attributes { get; set; } = [];

    ///<summary>
    /// the children of the element
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(2)]
    public List<HtmlNode> Children { get; set; } = [];

    /// <summary>
    /// implicit conversion from string to <see cref="HtmlTextNode"/>
    /// </summary>
    public static implicit operator HtmlNode(string value)
    {
        return new HtmlTextNode(value);
    }

    /// <summary>
    /// Constructs a new instance of <see cref="HtmlNode"/>
    /// </summary>
    public HtmlNode(string name,
            IEnumerable<HtmlAttribute>? attributes = null,
            IEnumerable<HtmlNode>? children = null) : base(name)
    {
        Name = name;
        if (children is not null)
        {
            Children = children.ToList();
        }

        if (attributes is not null)
        {
            Attributes = attributes.ToList();
        }
    }

    /// <summary>
    /// Constructs a new instance of <see cref="HtmlNode"/>
    /// </summary>
    public HtmlNode(string name, params HtmlItem[] children) : base(name)
    {
        Children = children.OfType<HtmlNode>().ToList();
        Attributes = children.OfType<HtmlAttribute>().ToList();
    }

    /// <summary>
    /// Constructs a new instance of <see cref="HtmlNode"/>
    /// </summary>
    public HtmlNode(string name, IEnumerable<HtmlItem> children) : base(name)
    {
        Children = children.OfType<HtmlNode>().ToList();
        Attributes = children.OfType<HtmlAttribute>().ToList();
    }

    /// <summary>
    /// Constructs a new instance of <see cref="HtmlNode"/>
    /// </summary>
    public HtmlNode(string name, string value) : base(name, value) { }

    /// <summary>
    /// Adds the given children to the element
    /// </summary>
    public HtmlNode Add(params HtmlItem[] children)
    {
        Children.AddRange(children.OfType<HtmlNode>());
        Attributes.AddRange(children.OfType<HtmlAttribute>());
        return this;
    }

    private static void MaybeCr(StringBuilder sb)
    {
        if (RenderOptions.Indent > 0 && sb.Length > 0 && sb[^1] != '\n')
        {
            _ = sb.AppendLine();
        }
    }

    private static string MaybeCr(string sb)
    {
        return RenderOptions.Indent switch
        {
            > 0 when sb.Length > 0 && sb[^1] != '\n' => sb + "\n",
            _ => sb,
        };
    }

    private const char OpenTag = '<';
    private const char CloseTag = '>';
    private const char Space = ' ';
    private const char Backslash = '/';

    ///<inheritdoc/>
    public override string ToString(int indent = 0)
    {
        StringBuilder sb = new();
        string intentStr = new(' ', indent);

        _ = sb.Append(intentStr);
        _ = sb.Append('<');
        _ = sb.Append(Name);
        if (Attributes.Count != 0)
        {
            _ = sb.Append(' ');
            _ = sb.Append(string.Join(" ", Attributes));
        }
        _ = sb.Append('>');
        MaybeCr(sb);
        foreach (HtmlNode child in Children)
        {
            _ = sb.Append(child?.ToString(indent + RenderOptions.Indent));
            MaybeCr(sb);
        }
        MaybeCr(sb);
        _ = sb.Append(intentStr);
        _ = sb.Append(OpenTag).Append(Backslash).Append(Name).Append(CloseTag);
        return sb.ToString();
    }


    ///<inheritdoc/>
    public override void AppendTo(ref StringBuilder sb, int indent = 0)
    {
        TextWriter sw = new StringWriter(sb);
        WriteTo(ref sw, indent);
    }

    ///<inheritdoc/>
    public override void WriteTo(ref TextWriter tw, int indent = 0)
    {
        string indentStr = new(' ', indent);
        tw.Write(indentStr);
        tw.Write(OpenTag);
        tw.Write(Name);
        foreach (HtmlAttribute attr in Attributes)
        {
            tw.Write(Space);
            attr.WriteTo(ref tw);
        }
        tw.Write(CloseTag);
        bool newLines = Children.Count > 0 && RenderOptions.Indent > 0;
        if (newLines)
        {
            tw.WriteLine();
        }

        foreach (HtmlNode? child in Children)
        {
            child?.WriteTo(ref tw, indent + RenderOptions.Indent);
            if (newLines)
            {
                if (child is HtmlTextNode)
                {
                    if (RenderOptions.TextNodeOnNewLine)
                    {
                        tw.WriteLine();
                    }
                }
                else
                {
                    tw.WriteLine();
                }
            }
            else if (newLines)
            {
                tw.WriteLine();
            }
        }
        tw.Write(indentStr);
        tw.Write(OpenTag);
        tw.Write(Backslash);
        tw.Write(Name);
        tw.Write(CloseTag);
    }

    ///<inheritdoc/>
    public override string ToString()
    {
        return ToString(0);
    }
}