namespace CC.CSX.Tests;
using static CC.CSX.Htmx.HtmxAttributes;

public class MultiHtmlAttributeTests
{
    [Fact]
    public void Should_RenderAll_AttributesTheSameAsIfTheyWereWrittenOneByOne()
    {
        var attrs = new HtmlAttribute[]
        {
            hxGet("/foo"),
            hxSwap("outerHTML"),
            hxTarget("#foo"),
            hxTrigger("click")
        };

        MultiHtmlAttribute multi = new MultiHtmlAttribute(attributes: attrs);
        var expected = "hx-get=\"/foo\" hx-swap=\"outerHTML\" hx-target=\"#foo\" hx-trigger=\"click\"";
        var actual = string.Join(" ", attrs.Select(a => a.ToString()));
        Assert.Equal(expected, actual);
        Assert.Equal(expected, multi.ToString());
    }
}
    