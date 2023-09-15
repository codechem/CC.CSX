namespace CC.CSX;

/// <summary>
/// A node that does not render itself, but just its children.
/// </summary>
public class Fragment : HtmlNode
{
    public Fragment(params HtmlItem[] children) : base("shallow", children) { }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <remarks>
    /// Because Shallow does not render itself, it does not take into account the indentation, and just passes it to its children.
    /// </remarks>
    public override string ToString(int indent = 0) => string.Join("", Children.Select(x => x.ToString(indent)));
}
