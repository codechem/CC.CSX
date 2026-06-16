using static CC.CSX.HtmlElements;
using static CC.CSX.Browser.HtmlEvents;

namespace CC.CSX.Browser.Tests;

public class HtmlEventsTests
{
    [Fact]
    public void DelegateOverloadProducesNumericHandlerAttribute()
    {
        var attr = onClick(() => { });
        Assert.Equal("data-ht-on-click", attr.Name);
        Assert.True(int.TryParse(attr.Value, out _));
    }

    [Fact]
    public void DomEventOverloadProducesNumericHandlerAttribute()
    {
        var attr = onInput((DomEvent e) => { });
        Assert.Equal("data-ht-on-input", attr.Name);
        Assert.True(int.TryParse(attr.Value, out _));
    }

    [Fact]
    public void HtActionUsesActionNameAsValue()
    {
        var attr = htAction("counter/reset");
        Assert.Equal("data-ht-on-click", attr.Name);
        Assert.Equal("counter/reset", attr.Value);
    }

    [Fact]
    public void HtActionSupportsOtherEvents()
    {
        var attr = htAction("form/save", "submit");
        Assert.Equal("data-ht-on-submit", attr.Name);
        Assert.Equal("form/save", attr.Value);
    }

    [Fact]
    public void GenericOnSupportsArbitraryEventNames()
    {
        var attr = on("pointerdown", () => { });
        Assert.Equal("data-ht-on-pointerdown", attr.Name);
    }

    [Fact]
    public void RenderedMarkupCarriesHandlerAttribute()
    {
        var html = Button(onClick(() => { }), "Increment").ToString();
        Assert.Contains("data-ht-on-click=", html);
        Assert.Contains("Increment", html);
    }
}
