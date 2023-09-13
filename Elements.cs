namespace CC.CSX;

public static class CustomElements
{
    public static HtmlNode A(string href, params HtmlItem[] children) => new HtmlNode("a").Add(("href", href)).Add(children);
    public static HtmlNode Img(string src, params HtmlItem[] children) => new HtmlNode("img", children).Add("src", src);
}