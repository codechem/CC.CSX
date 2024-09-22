using System.Text;

namespace CC.CSX;

/// <summary>
/// A node that has no children and renders nothing.
/// No instance of this class should be created, instead use <see cref="Instance"/>.
/// </summary>
public class HtmlNone : HtmlItem
{
    /// <summary>
    /// The singleton instance of <see cref="HtmlNone"/>.
    /// </summary>
    public static readonly HtmlNone Instance = new();
    private HtmlNone() : base("None") { }

    /// <inheritdoc/>
    public override string ToString()
    {
        return string.Empty;
    }

    /// <inheritdoc/>
    public override string ToString(int indent = 0)
    {
        return string.Empty;
    }

    /// <inheritdoc/>
    public override void AppendTo(ref StringBuilder sb, int indent = 0) { }

    /// <inheritdoc/>
    public override void WriteTo(ref TextWriter tw, int indent = 0) { }
}