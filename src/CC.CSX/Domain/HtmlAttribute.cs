namespace CC.CSX;
using System.Text;

public class HtmlAttribute : HtmlItem
{
    public HtmlAttribute(in string name, string? value) : base(name, value) { }
    public HtmlAttribute(in string name) : base(name) { }
    public override string ToString(int indent = 0) => Value is null ? Name : $"{Name}=\"{Value}\"";
    public override string ToString() => ToString(0);

    public override void AppendTo(ref StringBuilder sb, int indent = 0)
    {
        if (Value is null)
        {
            sb.Append(Name);
        }
        else
        {
            sb.Append(Name).Append('=').Append(Value);
        }
    }



    public static implicit operator HtmlAttribute((string key, string value) tuple) => new HtmlAttribute(tuple.key, tuple.value);
}