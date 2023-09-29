namespace CC.CSX;
using System.Text;

/// <summary>
/// A node that has no children and renders nothing.
/// No instance of this class should be created, instead use <see cref="HtmlNone.Instance"/>.
/// </summary>
public class HtmlNone : HtmlItem
{
    /// <summary>
    /// The singleton instance of <see cref="HtmlNone"/>.
    /// </summary>
    public static readonly HtmlNone Instance = new HtmlNone();
    private HtmlNone() : base("None") { }

    /// <inheritdoc/>
    public override string ToString() => string.Empty;

    /// <inheritdoc/>
    public override string ToString(int indent = 0) => string.Empty;
    
    /// <inheritdoc/>
    public override void AppendTo(ref StringBuilder sb, int indent = 0) { }

    /// <inheritdoc/>
    public override void WriteTo(ref TextWriter tw, int indent = 0) { }
}