namespace CC.CSX.Browser.Tests;

public class HxRouterTests : IDisposable
{
    public HxRouterTests() => HxRouter.Clear();
    public void Dispose() => HxRouter.Clear();

    static readonly Func<HxRequest, HtmlItem?> Noop = _ => null;

    [Fact]
    public void ExactPathMatches()
    {
        HxRouter.Add("POST", "/todos", Noop);
        Assert.NotNull(HxRouter.Match("POST", "/todos"));
        Assert.Null(HxRouter.Match("GET", "/todos"));
        Assert.Null(HxRouter.Match("POST", "/other"));
    }

    [Fact]
    public void TrailingSlashesAreTolerated()
    {
        HxRouter.Add("GET", "todos/", Noop);
        Assert.NotNull(HxRouter.Match("GET", "/todos"));
    }

    [Fact]
    public void TemplateSegmentsCaptureAndUnescape()
    {
        HxRouter.Add("DELETE", "/todos/{title}", Noop);
        var match = HxRouter.Match("DELETE", "/todos/buy%20milk");
        Assert.NotNull(match);
        Assert.Equal("buy milk", match.Value.PathParams["title"]);
    }

    [Fact]
    public void SegmentCountMustMatch()
    {
        HxRouter.Add("GET", "/todos/{id}", Noop);
        Assert.Null(HxRouter.Match("GET", "/todos"));
        Assert.Null(HxRouter.Match("GET", "/todos/1/details"));
    }

    [Fact]
    public void QueryStringIsStrippedFromTemplatesAndParsed()
    {
        HxRouter.Add("GET", "/inc?step=2", Noop);
        Assert.NotNull(HxRouter.Match("GET", "/inc"));

        var (path, query) = HxRouter.SplitQuery("/inc?step=2&label=a%20b&flag");
        Assert.Equal("/inc", path);
        Assert.Equal("2", query["step"]);
        Assert.Equal("a b", query["label"]);
        Assert.Equal("", query["flag"]);
    }

    [Fact]
    public void FirstRegisteredRouteWins()
    {
        Func<HxRequest, HtmlItem?> first = _ => null;
        Func<HxRequest, HtmlItem?> second = _ => null;
        HxRouter.Add("GET", "/x", first);
        HxRouter.Add("GET", "/x", second);
        Assert.Same(first, HxRouter.Match("GET", "/x")!.Value.Handler);
    }
}
