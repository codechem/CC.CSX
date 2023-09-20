namespace CC.CSX;

public class HtmlAttribute : HtmlItem
{
    public HtmlAttribute(string name, string? value) : base(name, value) { }
    public HtmlAttribute(string name) : base(name) { }
    public override string ToString(int indent = 0) => Value is null ? Name : $"{Name}=\"{Value}\"";
    public override string ToString() => ToString(0);

    public static implicit operator HtmlAttribute((string key, string value) tuple) => new HtmlAttribute(tuple.key, tuple.value);
}