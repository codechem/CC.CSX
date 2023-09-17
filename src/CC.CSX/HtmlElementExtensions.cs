namespace CC.CSX;
public static class HtmlElementExtensions
{
    public static HtmlNode WithParent(HtmlNode child, HtmlNode parent, string placeholderId)
        => parent.Placeholders[placeholderId].Add(child);
}

public class HtmlPlacehoder : HtmlItem
{
    public List<HtmlNode> Children{get; private set;}= new();
    public HtmlPlacehoder(string name) : base(name)
    {
    }

    public HtmlPlacehoder(string name, string? value) : base(name, value)
    {
    }

    public override string ToString(int indent = 0)
    {
        if(Children.Any()){
            return new Fragment(children);
        }
    }
}