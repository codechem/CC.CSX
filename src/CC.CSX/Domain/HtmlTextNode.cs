namespace CC.CSX;

/// <summary>
/// A node that renders itself as a regular text
/// </summary>
public class HtmlTextNode : HtmlNode
{
    public HtmlTextNode(string value) : base("#text", value) { }

    public override string ToString() => Value ?? "";
    ///<summary>
    ///<inheritdoc/>
    ///<summary>
    public override string ToString(int indent = 0) => (new string(' ', indent) + Value) ?? "";
}
