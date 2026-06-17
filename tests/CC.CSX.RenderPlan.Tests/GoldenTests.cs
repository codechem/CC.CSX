namespace CC.CSX.RenderPlan.Tests;

using System.Buffers;
using System.Text;

using CC.CSX;

using RenderPlanSpike;

using Xunit;

// The generated optimized writer must produce byte-identical output to the original method's
// WriteTo. (WithUnknown is intentionally omitted — it calls DateTime.Now, so it isn't deterministic.)
public class GoldenTests
{
    public GoldenTests() => RenderOptions.Indent = 0; // plans assume compact output

    static string Live(HtmlNode node)
    {
        var b = new ArrayBufferWriter<byte>();
        node.WriteTo(b);
        return Encoding.UTF8.GetString(b.WrittenSpan);
    }

    static string Optimized(System.Action<ArrayBufferWriter<byte>> write)
    {
        var b = new ArrayBufferWriter<byte>();
        write(b);
        return Encoding.UTF8.GetString(b.WrittenSpan);
    }

    [Fact]
    public void UserRow_Matches()
    {
        Assert.Equal(
            Live(Views.UserRow(7, "Ann", "ann@example.com")),
            Optimized(b => Views__Optimized.UserRow(7, "Ann", "ann@example.com", b)));
    }

    [Fact]
    public void TableHeader_Matches()
    {
        Assert.Equal(
            Live(Views.TableHeader()),
            Optimized(b => Views__Optimized.TableHeader(b)));
    }

    [Fact]
    public void Report_Matches()
    {
        var rows = new[] { (0, "n0", "u0@x.com"), (1, "n1", "u1@x.com"), (2, "n2", "u2@x.com") };
        Assert.Equal(
            Live(Views.Report(rows)),
            Optimized(b => Views__Optimized.Report(rows, b)));
    }

    [Fact]
    public void Profile_Matches()
    {
        Assert.Equal(
            Live(Views.Profile("costa")),
            Optimized(b => Views__Optimized.Profile("costa", b)));
    }
}
