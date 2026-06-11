namespace CC.CSX.Css.Tests;

using static CC.CSX.Css.Tailwind.Tw;

public class TailwindTests
{
    [Fact]
    public void Constants_Should_RenderTailwindClassNames()
    {
        Assert.Equal("flex", (string)flex);
        Assert.Equal("p-0.5", (string)p0_5);
        Assert.Equal("w-1/2", (string)w1of2);
        Assert.Equal("bg-red-500", (string)bgRed500);
        Assert.Equal("static", (string)@static);
    }

    [Fact]
    public void Utilities_Should_ComposeIntoVirtualClasses()
    {
        var card = bgWhite + roundedLg + shadowMd + p4;
        Assert.Equal("bg-white rounded-lg shadow-md p-4", card.Name);
    }

    [Fact]
    public void Variants_Should_PrefixEveryToken()
    {
        Assert.Equal("hover:bg-sky-600", hover(bgSky600).Name);
        Assert.Equal("hover:bg-sky-600 hover:underline", hover(bgSky600 + underline).Name);
    }

    [Fact]
    public void Variants_Should_Nest()
    {
        Assert.Equal("md:hover:bg-sky-600", md(hover(bgSky600)).Name);
        Assert.Equal("dark:bg-slate-800", dark(bgSlate800).Name);
    }

    [Fact]
    public void ArbitraryValues_Should_RenderBracketSyntax()
    {
        Assert.Equal("mt-[17px]", mt("17px").Name);
        Assert.Equal("grid-cols-[1fr_2fr]", gridCols("1fr_2fr").Name);
        Assert.Equal("aria-busy:p-4", variant("aria-busy", p4).Name);
    }

    [Fact]
    public void NegAndImportant_Should_PrefixTokens()
    {
        Assert.Equal("-mt-4", neg(mt4).Name);
        Assert.Equal("!mt-4", important(mt4).Name);
    }

    [Fact]
    public void TailwindClasses_Should_WorkInClassAttribute()
    {
        RenderOptions.Indent = 0;
        var node = Div(@class(flex, itemsCenter, gap2, "custom"));
        Assert.Equal("<div class=\"flex items-center gap-2 custom\"></div>", node.ToString());
    }
}
