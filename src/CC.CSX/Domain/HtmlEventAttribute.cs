namespace CC.CSX;

///<summary>Represents an HTML event attribute.</summary>
public class HtmlEventAttribute : HtmlAttribute
{
    public HtmlEventAttribute(string name) : base(name)
    {
    }

    public HtmlEventAttribute(string name, string? value) : base(name, value)
    {
    }

    public HtmlEventAttribute(string name, Action<EventArgs> handler) : base(name)
    {
    }
}