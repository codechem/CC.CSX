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
    public override string ToString()
    {
        return Value ?? "";
    }

    ///<summary>
    ///<inheritdoc/>
    ///</summary>
    public override string ToString(int indent = 0)
    {
        return (new string(' ', indent) + Value) ?? "";
    }

    ///<inheritdoc/>
    public override void AppendTo(ref StringBuilder sb, int indent = 0)
    {
        _ = indent switch
        {
            <= 0 => sb.Append(Value),
            _ => sb.Append(new string(' ', indent)).Append(Value),
        };
    }

    ///<inheritdoc/>
    public override void WriteTo(ref TextWriter tw, int indent = 0)
    {
        if (Value is not null)
        {
            if (indent > 0)
            {
                tw.Write(new string(' ', indent));
                tw.Write(Value);
            }
            else
            {
                tw.Write(Value);
            }
        }
    }
}