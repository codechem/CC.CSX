namespace CC.CSX;
using System.Text;

/// <summary>
/// A node that has no children and renders nothing.
/// </summary>
public class HtmlNone : HtmlItem
{
    public static readonly HtmlNone Instance = new HtmlNone();
    private HtmlNone() : base("None") { }
    public override string ToString() => string.Empty;
    public override string ToString(int indent = 0) => string.Empty;
    public override void AppendTo(ref StringBuilder sb, int indent = 0) { }
}