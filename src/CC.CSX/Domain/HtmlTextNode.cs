namespace CC.CSX;
using System.Text;

///<summary>
/// A node that renders itself as a regular text
///</summary>
public class HtmlTextNode : HtmlNode
{
    const string textNodeKey = "#text";
    /// <summary>
    /// Creates a new instance of <see cref="HtmlTextNode"/> with the given value.
    /// </summary>
    public HtmlTextNode(in string value) : base(textNodeKey, value) { }

    /// <summary>
    /// Creates a new instance of <see cref="HtmlTextNode"/> with the given value.
    /// </summary>
    public override string ToString() => Value ?? "";

    ///<summary>
    ///<inheritdoc/>
    ///</summary>
    public override string ToString(int indent = 0) => (new string(' ', indent) + Value) ?? "";

    ///<inheritdoc/>
    public override void AppendTo(ref StringBuilder sb, int indent = 0)
    {
        if (indent > 0)
            sb.Append(new string(' ', indent)).Append(Value);
        else
            sb.Append(Value);
    }


    ///<inheritdoc/>
    public override void WriteTo(ref TextWriter sb, int indent = 0)
    {
        if (Value is null)
            return;

        if (indent > 0)
        {
            sb.Write(new string(' ', indent));
            sb.Write(Value);
        }
        else
        {
            sb.Write(Value);
        }
    }
}