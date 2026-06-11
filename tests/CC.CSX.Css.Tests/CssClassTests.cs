namespace CC.CSX.Css.Tests;

public class CssClassTests
{
    static readonly CssClass Card = new("card");
    static readonly CssClass Highlighted = new("card--highlighted");
    static readonly CssClass Shadow = new("shadow");

    [Fact]
    public void ImplicitStringConversion_Should_ReturnName()
    {
        string name = Card;
        Assert.Equal("card", name);
    }

    [Fact]
    public void PlainStrings_Should_StillWorkIn_ClassAttribute()
    {
        RenderOptions.Indent = 0;
        var node = Div(@class(Card, "mt-2"));
        Assert.Equal("<div class=\"card mt-2\"></div>", node.ToString());
    }

    [Fact]
    public void Compose_Should_JoinAndDeduplicate()
    {
        var composed = CssClass.Compose(Card, Highlighted, Card, Shadow);
        Assert.Equal("card card--highlighted shadow", composed.Name);
    }

    [Fact]
    public void PlusOperator_Should_BuildVirtualClass()
    {
        var featured = Card + Highlighted + "rounded";
        Assert.Equal("card card--highlighted rounded", featured.Name);
    }

    [Fact]
    public void ArrayOfClasses_Should_ExpandToSingleVirtualClass()
    {
        CssClass virtualClass = new[] { Card, Highlighted, Shadow };
        Assert.Equal("card card--highlighted shadow", virtualClass.Name);
    }

    [Fact]
    public void CssClassInElementPosition_Should_BecomeClassAttribute()
    {
        RenderOptions.Indent = 0;
        var node = Div(Card + Shadow, H1("hi"));
        Assert.Equal("<div class=\"card shadow\"><h1>hi</h1></div>", node.ToString());
    }

    [Fact]
    public void None_Should_HaveEmptyName()
    {
        Assert.Equal(string.Empty, CssClass.None.Name);
        Assert.Empty(CssClass.None.Tokens);
    }

    [Fact]
    public void Tokens_Should_SplitComposedClasses()
    {
        var featured = Card + Highlighted;
        Assert.Equal(["card", "card--highlighted"], featured.Tokens);
    }
}
