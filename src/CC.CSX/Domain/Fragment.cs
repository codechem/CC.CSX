using System.Text;

namespace CC.CSX;

/// <summary>
/// A node that does not render itself, but just its children.
/// </summary>
public class Fragment : HtmlNode
{
    /// <summary>
    /// The singleton instance of <see cref="Fragment"/> that has no children.
    /// </summary>
    public static Fragment Empty { get; } = new();

    /// <summary>
    /// Creates a new instance of <see cref="Fragment"/> with the given children.
    /// </summary>
    public Fragment(params HtmlNode[] children) : base("shallow", children) { }

    /// <summary>
    /// Creates a new instance of <see cref="Fragment"/> with the given children.
    /// </summary>
    public Fragment(IEnumerable<HtmlNode> children) : base("shallow", children: children) { }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <remarks>
    /// Because Shallow does not render itself, it does not take into account the indentation, and just passes it to its children.
    /// </remarks>
    public override string ToString(int indent = 0) =>
        RawChildren is { } children ? string.Join("", children.Select(x => x.ToString(indent))) : string.Empty;

    ///<inheritdoc/>
    public override void AppendTo(ref StringBuilder sb, int indent = 0)
    {
        if (RawChildren is not { } children) return;
        foreach (var child in children)
        {
            child.AppendTo(ref sb, indent);
            if (RenderOptions.Indent > 0)
                sb.AppendLine();
        }
    }

    ///<inheritdoc/>
    public override void WriteTo(ref TextWriter tw, int indent = 0)
    {
        if (RawChildren is not { } children) return;
        foreach (var child in children)
        {
            child.WriteTo(ref tw, indent);
            if (RenderOptions.Indent > 0)
                tw.WriteLine();
        }
    }
}