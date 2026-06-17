namespace CC.CSX.Tests;

using Xunit;

using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;

public class EscapingTests
{
    public EscapingTests() => RenderOptions.Indent = 0;

    [Fact]
    public void TextContent_IsEscaped()
    {
        var html = Div(P("a < b & c > d")).ToString();
        Assert.Contains("a &lt; b &amp; c &gt; d", html);
        Assert.DoesNotContain("< b", html);
    }

    [Fact]
    public void AttributeValue_IsEscaped()
    {
        var html = A(href("/s?a=1&b=2"), title("say \"hi\"")).ToString();
        Assert.Contains("href=\"/s?a=1&amp;b=2\"", html);
        Assert.Contains("title=\"say &quot;hi&quot;\"", html);
    }

    [Fact]
    public void Raw_IsNotEscaped()
    {
        var html = Div(Raw("<b>bold &amp; raw</b>")).ToString();
        Assert.Contains("<b>bold &amp; raw</b>", html);
    }

    [Fact]
    public void StyleContent_IsNotEscaped()
    {
        // raw-text element: CSS like `a > b {}` must survive verbatim
        var html = Style("a > b { content: \"x\"; }").ToString();
        Assert.Contains("a > b { content: \"x\"; }", html);
        Assert.DoesNotContain("&gt;", html);
    }

    [Fact]
    public void ScriptContent_IsNotEscaped()
    {
        var html = Script("if (a < b && c) {}").ToString();
        Assert.Contains("if (a < b && c) {}", html);
        Assert.DoesNotContain("&lt;", html);
    }
}
