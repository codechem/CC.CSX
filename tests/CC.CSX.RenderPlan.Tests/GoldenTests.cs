namespace CC.CSX.RenderPlan.Tests;

using System.Buffers;
using System.Text;

using CC.CSX;

using RenderPlanSpike;

using Xunit;

// The generated optimized builder (Views__Optimized.*) returns a PlanNode whose render must be
// byte-identical to the original method's WriteTo. (WithUnknown is omitted — DateTime.Now is
// non-deterministic.)
public class GoldenTests
{
    public GoldenTests() => RenderOptions.Indent = 0; // plans assume compact output

    static string Render(HtmlNode node)
    {
        var b = new ArrayBufferWriter<byte>();
        node.WriteTo(b);
        return Encoding.UTF8.GetString(b.WrittenSpan);
    }

    [Fact]
    public void UserRow_Matches()
        => Assert.Equal(
            Render(Views.UserRow(7, "Ann", "ann@example.com")),
            Render(Views__Optimized.UserRow(7, "Ann", "ann@example.com")));

    [Fact]
    public void TableHeader_Matches()
        => Assert.Equal(Render(Views.TableHeader()), Render(Views__Optimized.TableHeader()));

    [Fact]
    public void Report_Matches()
    {
        var rows = new[] { (0, "n0", "u0@x.com"), (1, "n1", "u1@x.com"), (2, "n2", "u2@x.com") };
        Assert.Equal(Render(Views.Report(rows)), Render(Views__Optimized.Report(rows)));
    }

    [Fact]
    public void Profile_Matches()
        => Assert.Equal(Render(Views.Profile("costa")), Render(Views__Optimized.Profile("costa")));

    [Fact]
    public void Status_StructuralConditional_Matches()
    {
        Assert.Equal(Render(Views.Status(true)), Render(Views__Optimized.Status(true)));
        Assert.Equal(Render(Views.Status(false)), Render(Views__Optimized.Status(false)));
    }

    [Fact]
    public void OptimizedBuilder_ReturnsPlanNode()
        => Assert.IsType<PlanNode>(Views__Optimized.UserRow(1, "a", "b"));
}
