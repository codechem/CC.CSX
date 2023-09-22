namespace CC.CSX;
using System.Text;

/// <summary>
/// A node that renders itself as a regular text
/// </summary>
public class HtmlTextNode : HtmlNode
{
    const string textNodeKey = "#text";
    public HtmlTextNode(in string value) : base(textNodeKey, value) { }

    public override string ToString() => Value ?? "";

    ///<summary>
    ///<inheritdoc/>
    ///<summary>
    public override string ToString(int indent = 0) => (new string(' ', indent) + Value) ?? "";

    public override void AppendTo(ref StringBuilder sb, int indent = 0)
    {
        if (indent > 0)
            sb.Append(new string(' ', indent)).Append(Value);
        else
            sb.Append(Value);
    }
}