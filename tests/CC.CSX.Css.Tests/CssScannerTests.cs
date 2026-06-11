namespace CC.CSX.Css.Tests;

using CC.CSX.Css.Generator;

public class CssScannerTests
{
    [Fact]
    public void ExtractClassNames_Should_FindSimpleSelectors()
    {
        var names = CssScanner.ExtractClassNames(".card { color: red; } .btn, .btn-primary { margin: 0; }");
        Assert.Equal(["card", "btn", "btn-primary"], names);
    }

    [Fact]
    public void ExtractClassNames_Should_DeduplicatePreservingOrder()
    {
        var names = CssScanner.ExtractClassNames(".a { } .b { } .a:hover { }");
        Assert.Equal(["a", "b"], names);
    }

    [Fact]
    public void ExtractClassNames_Should_IgnoreComments_Strings_AndUrls()
    {
        var css = """
            /* .commented { } */
            .real { background: url(images/x.png); content: ".fake"; }
            @import "other.css";
            """;
        var names = CssScanner.ExtractClassNames(css);
        Assert.Equal(["real"], names);
    }

    [Fact]
    public void ExtractClassNames_Should_NotTreatDecimalsAsClasses()
    {
        var names = CssScanner.ExtractClassNames(".w { width: .5em; opacity: 0.5; }");
        Assert.Equal(["w"], names);
    }

    [Fact]
    public void ExtractClassNames_Should_HandleNestingAndCombinators()
    {
        var css = ".parent { > .child { } &.modifier { } } div.tagged ~ .sibling { }";
        var names = CssScanner.ExtractClassNames(css);
        Assert.Equal(["parent", "child", "modifier", "tagged", "sibling"], names);
    }

    [Theory]
    [InlineData("card", "card")]
    [InlineData("card--highlighted", "cardHighlighted")]
    [InlineData("btn-primary", "btnPrimary")]
    [InlineData("text_center", "textCenter")]
    [InlineData("-webkit-thing", "webkitThing")]
    [InlineData("2cols", "_2cols")]
    public void ToCSharpIdentifier_Should_CamelCase(string css, string expected)
        => Assert.Equal(expected, CssScanner.ToCSharpIdentifier(css));
}
