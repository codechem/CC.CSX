namespace CC.CSX.Tests;

using System.Buffers;
using System.Text;

using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;

public class RenderPlanTests
{
    static string Live(HtmlNode node)
    {
        var buf = new ArrayBufferWriter<byte>();
        node.WriteTo(buf);
        return Encoding.UTF8.GetString(buf.WrittenSpan);
    }

    static void AssertPlanMatchesLive(HtmlNode view)
    {
        RenderOptions.Indent = 0;
        var plan = RenderPlan.Compile(view);
        Assert.Equal(Live(view), plan.Render());
    }

    [Fact]
    public void StaticOnly_MatchesLive()
        => AssertPlanMatchesLive(Div(@class("box"), H1("Title"), P("body text")));

    [Fact]
    public void SingleHole_Interleaved_MatchesLive()
    {
        var count = 42;
        AssertPlanMatchesLive(
            Div(Span("before"), Dyn(() => P($"count={count}")), Span("after")));
    }

    [Fact]
    public void HoleAtTagBoundary_MatchesLive()
    {
        // hole sits between the opening <div> and a trailing static node — exercises mid-node cut
        AssertPlanMatchesLive(Div(Dyn(() => "x"), Hr()));
    }

    [Fact]
    public void Each_MatchesLive()
    {
        var items = new[] { "a", "b", "c" };
        AssertPlanMatchesLive(Ul(Each(items, i => Li(@class("row"), i))));
    }

    [Fact]
    public void Nested_StaticDynamicMix_MatchesLive()
    {
        var rows = Enumerable.Range(0, 5).ToArray();
        AssertPlanMatchesLive(
            Div(@class("uk-container"),
                H1("Report"),
                Table(@class("uk-table"),
                    Thead(Tr(Th("Id"), Th("Name"))),
                    Tbody(Each(rows, i => Tr(@class(i % 2 == 0 ? "even" : "odd"), Td(i), Td($"name-{i}"))))),
                Dyn(() => P("footer"))));
    }

    [Fact]
    public void Plan_SegmentsAreCoalesced()
    {
        RenderOptions.Indent = 0;
        // div( static, hole, static ) -> [static, hole, static] = 3 segments
        var plan = RenderPlan.Compile(Div(Span("a"), Dyn(() => "b"), Span("c")));
        Assert.Equal(3, plan.SegmentCount);
    }
}
