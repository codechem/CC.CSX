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

    // Backing stores are left null until something is actually added, so leaf nodes (the bulk of a
    // large document — table cells, text wrappers) don't each pay for two empty List allocations.
    private List<HtmlAttribute>? _attributes;
    private List<HtmlNode>? _children;

    /// <summary>
    /// The element's attributes. Accessing this getter materializes the backing list; render/
    /// traversal paths read <see cref="RawAttributes"/> instead so they never force an allocation.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(1)]
    public List<HtmlAttribute> Attributes { get => _attributes ??= []; set => _attributes = value; }

    ///<summary>
    /// the children of the element. Accessing this getter materializes the backing list; render/
    /// traversal paths read <see cref="RawChildren"/> instead so they never force an allocation.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(2)]
    public List<HtmlNode> Children { get => _children ??= []; set => _children = value; }

    /// <summary>The attributes list if one has been allocated, otherwise null (no allocation).</summary>
    internal List<HtmlAttribute>? RawAttributes => _attributes;

    /// <summary>The children list if one has been allocated, otherwise null (no allocation).</summary>
    internal List<HtmlNode>? RawChildren => _children;

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
        // left null when not supplied, so empty collections cost nothing
        if (children is not null) _children = children.ToList();
        if (attributes is not null) _attributes = attributes.ToList();
    }

    /// <summary>
    /// Constructs a new instance of <see cref="HtmlNode"/>
    /// </summary>
    public HtmlNode(string name, params HtmlItem[] children) : base(name)
        => Partition(children, out _children, out _attributes);

    /// <summary>
    /// Constructs a new instance of <see cref="HtmlNode"/>
    /// </summary>
    public HtmlNode(string name, IEnumerable<HtmlItem> children) : base(name)
        => Partition(children, out _children, out _attributes);

    // Splits a mixed item list into child nodes and attributes in a single pass, preserving order.
    // Replaces two LINQ OfType().ToList() passes; each list is allocated only if it gets an item, so
    // a leaf node (no children, no attributes) allocates neither.
    private static void Partition(IEnumerable<HtmlItem> items,
        out List<HtmlNode>? nodes, out List<HtmlAttribute>? attributes)
    {
        nodes = null;
        attributes = null;
        foreach (HtmlItem item in items)
        {
            switch (item)
            {
                case HtmlAttribute a: (attributes ??= []).Add(a); break;
                case HtmlNode n: (nodes ??= []).Add(n); break;
            }
        }
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
        foreach (HtmlItem item in children)
        {
            switch (item)
            {
                case HtmlAttribute a: Attributes.Add(a); break;
                case HtmlNode n: Children.Add(n); break;
            }
        }
        return this;
    }

    private const char OpenTag = '<';
    private const char CloseTag = '>';
    private const char Space = ' ';
    private const char Backslash = '/';

    ///<inheritdoc/>
    /// <remarks>
    /// Renders the whole subtree into a single <see cref="StringBuilder"/> via <see cref="WriteTo"/>,
    /// rather than allocating a StringBuilder and intermediate string per node. The test suite
    /// pins <c>ToString</c> output equal to <c>WriteTo</c> output.
    /// </remarks>
    public override string ToString(int indent = 0)
    {
        StringBuilder sb = new();
        AppendTo(ref sb, indent);
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
        Indentation.WriteTo(tw, indent);
        tw.Write(OpenTag);
        tw.Write(Name);
        if (_attributes is { } attrs)
        {
            foreach (HtmlAttribute attr in attrs)
            {
                tw.Write(Space);
                attr.WriteTo(ref tw);
            }
        }
        tw.Write(CloseTag);
        List<HtmlNode>? children = _children;
        bool newLines = children is { Count: > 0 } && RenderOptions.Indent > 0;
        if (newLines)
        {
            tw.WriteLine();
        }

        if (children is not null)
        {
            foreach (HtmlNode? child in children)
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
            }
        }
        Indentation.WriteTo(tw, indent);
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