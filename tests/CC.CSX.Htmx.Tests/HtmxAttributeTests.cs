namespace CC.CSX.Tests;

public class MultiHtmlAttributeTests
{
    [Fact]
    public void Should_RenderAll_AttributesTheSameAsIfTheyWereWrittenOneByOne()
    {
        var multiAttr = hxGet("/sample", target: "that", swap: "outerHTML", history: true, pushUrl: true, replaceUrl: true);
        var str =  multiAttr.ToString();

        Assert.True(multiAttr.Attributes.Count == 6);
    }
}