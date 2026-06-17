namespace CC.CSX.Tests;

using System.Buffers;
using System.Text;

using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;

public class FragmentCacheTests
{
    static string Utf8(HtmlNode node)
    {
        var buf = new ArrayBufferWriter<byte>();
        node.WriteTo(buf);
        return Encoding.UTF8.GetString(buf.WrittenSpan);
    }

    [Fact]
    public void Raw_RendersVerbatim_AcrossAllPaths()
    {
        RenderOptions.Indent = 0;
        var raw = Raw("<b>hi & bye</b>");
        Assert.Equal("<b>hi & bye</b>", raw.ToString());
        var sb = new StringBuilder();
        raw.AppendTo(ref sb);
        Assert.Equal("<b>hi & bye</b>", sb.ToString());
        Assert.Equal("<b>hi & bye</b>", Utf8(raw));
    }

    [Fact]
    public void CachedFragment_ProducesSameOutputAsLive_WhenEnabled()
    {
        RenderOptions.Indent = 0;
        FragmentCache.Enabled = true;
        var live = Div(@class("nav"), A(href("/"), "Home"), A(href("/x"), "X"));
        var cached = Div(@class("nav"), A(href("/"), "Home"), A(href("/x"), "X")).Cache();
        Assert.Equal(live.ToString(), cached.ToString());
        Assert.Equal(Utf8(live), Utf8(cached));
    }

    [Fact]
    public void CachedFragment_RendersLive_WhenDisabled()
    {
        RenderOptions.Indent = 0;
        FragmentCache.Enabled = false;
        try
        {
            var source = Div(P("a"));
            var cached = source.Cache();
            Assert.Equal(source.ToString(), cached.ToString());
            Assert.Equal(Utf8(source), Utf8(cached));
        }
        finally { FragmentCache.Enabled = true; }
    }

    [Fact]
    public void GetOrAdd_ReturnsSameInstance_WhenEnabled()
    {
        FragmentCache.Enabled = true;
        FragmentCache.Clear();
        var a = FragmentCache.GetOrAdd("k", () => Div(P("x")));
        var b = FragmentCache.GetOrAdd("k", () => Div(P("DIFFERENT")));
        Assert.Same(a, b); // second factory not invoked; cached instance reused
        Assert.Equal("<div><p>x</p></div>", Utf8(a).Replace("\r\n", "\n"));
        FragmentCache.Clear();
    }
}
