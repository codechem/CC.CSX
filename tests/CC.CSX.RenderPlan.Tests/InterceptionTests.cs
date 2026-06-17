namespace CC.CSX.RenderPlan.Tests;

using System.Buffers;
using System.Text;

using CC.CSX;

using RenderPlanSpike;

using Xunit;

// Demo.* call [RenderOptimized] views inside RenderPlanSpike, where interceptors are enabled. So the
// `Views.X(...)` calls there are rewritten to the optimized builder and return a PlanNode. (This test
// project has no interceptors, so calling Views.X(...) here yields the real tree — our oracle.)
public class InterceptionTests
{
    public InterceptionTests() => RenderOptions.Indent = 0;

    static string Render(HtmlNode node)
    {
        var b = new ArrayBufferWriter<byte>();
        node.WriteTo(b);
        return Encoding.UTF8.GetString(b.WrittenSpan);
    }

    [Fact]
    public void CallSite_IsIntercepted_ReturnsPlanNode()
    {
        Assert.IsType<PlanNode>(Demo.UserRow());
        Assert.IsType<PlanNode>(Demo.Status(true));
    }

    [Fact]
    public void Intercepted_UserRow_OutputMatchesOriginal()
        => Assert.Equal(
            Render(Views.UserRow(1, "Ann", "ann@example.com")), // real tree (not intercepted here)
            Render(Demo.UserRow()));                            // intercepted -> optimized plan

    [Fact]
    public void Intercepted_Status_OutputMatchesOriginal()
    {
        Assert.Equal(Render(Views.Status(true)), Render(Demo.Status(true)));
        Assert.Equal(Render(Views.Status(false)), Render(Demo.Status(false)));
    }

    [Fact]
    public void Intercepted_Report_OutputMatchesOriginal()
    {
        var rows = new[] { (0, "n0", "u0@x.com"), (1, "n1", "u1@x.com") };
        Assert.Equal(Render(Views.Report(rows)), Render(Demo.Report(rows)));
    }
}
