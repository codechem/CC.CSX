namespace CC.CSX.Css.Tests;

using static CC.CSX.Css.CssAttributes;
using static CC.CSX.Css.CssProperties;

public class CssStyleTests
{
    [Fact]
    public void TypedStyle_Should_RenderDeclarations()
    {
        var attribute = style(display("flex"), padding(8.px()), color("#336"));
        Assert.Equal("style=\"display:flex;padding:8px;color:#336\"", attribute.ToString());
    }

    [Fact]
    public void StringStyle_Should_StillWork()
    {
        var attribute = HtmlAttributes.style("display:flex");
        Assert.Equal("style=\"display:flex\"", attribute.ToString());
    }

    [Fact]
    public void Declarations_Should_MixWithTuples()
    {
        CssDeclaration fromTuple = ("margin", "0 auto");
        Assert.Equal("margin:0 auto", fromTuple.ToString());
        (string key, string value) = backgroundColor("red");
        Assert.Equal(("background-color", "red"), (key, value));
    }

    [Fact]
    public void Units_Should_FormatInvariant()
    {
        Assert.Equal("1.5rem", 1.5.rem());
        Assert.Equal("50%", 50.pct());
        Assert.Equal("250ms", 250.ms());
        Assert.Equal("2fr", 2.fr());
    }

    [Fact]
    public void TypedStyle_Should_RenderInsideElement()
    {
        RenderOptions.Indent = 0;
        var node = Div(style(textAlign("center"), maxWidth(40.rem())));
        Assert.Equal("<div style=\"text-align:center;max-width:40rem\"></div>", node.ToString());
    }
}
