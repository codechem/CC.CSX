namespace CC.CSX.Tests;

public class MultiHtmlAttributeTests
{
    [Fact]
    public void Should_RenderAll_AttributesTheSameAsIfTheyWereWrittenOneByOne()
    {
        var attrs = new HtmlAttribute[]
        {
            @class("/foo"),
            id("outerHTML"),
            style("#foo"),
        };

        MultiHtmlAttribute multi = new MultiHtmlAttribute(attributes: attrs);
        var expected = "class=\"/foo\" id=\"outerHTML\" style=\"#foo\"";
        var actual = string.Join(" ", attrs.Select(a => a.ToString()));
        Assert.Equal(expected, actual);
        Assert.Equal(expected, multi.ToString());
    }
}