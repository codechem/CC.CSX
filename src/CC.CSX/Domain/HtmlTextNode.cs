using System.Text;

namespace CC.CSX;

///<summary>
/// A node that renders itself as a regular text
///</summary>
/// <remarks>
/// Creates a new instance of <see cref="HtmlTextNode"/> with the given value.
/// </remarks>
public class HtmlTextNode(in string value) : HtmlNode(TextNodeKey, value)
{
    private const string TextNodeKey = "#text";

    /// <summary>
    /// Creates a new instance of <see cref="HtmlTextNode"/> with the given value.
    /// </summary>
    public override string ToString() => HtmlEscape.Escape(Value);

    ///<summary>
    ///<inheritdoc/>
    ///</summary>
    public override string ToString(int indent = 0)
    {
        var sb = new StringBuilder();
        if (indent > 0) Indentation.AppendTo(sb, indent);
        HtmlEscape.AppendEscaped(sb, Value);
        return sb.ToString();
    }

    ///<inheritdoc/>
    public override void AppendTo(ref StringBuilder sb, int indent = 0)
    {
        if (indent > 0) Indentation.AppendTo(sb, indent);
        HtmlEscape.AppendEscaped(sb, Value);
    }

    ///<inheritdoc/>
    public override void WriteTo(ref TextWriter tw, int indent = 0)
    {
        if (Value is not null)
        {
            if (indent > 0) Indentation.WriteTo(tw, indent);
            HtmlEscape.WriteEscaped(tw, Value);
        }
    }
}