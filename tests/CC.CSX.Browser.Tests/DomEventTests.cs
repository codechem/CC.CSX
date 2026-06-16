using System.Text.Json;

namespace CC.CSX.Browser.Tests;

public class DomEventTests
{
    [Fact]
    public void DeserializesDragPayload()
    {
        var json = """{"type":"drop","targetId":"bin","dataTransfer":"item-7"}""";
        var evt = JsonSerializer.Deserialize(json, DomEventJsonContext.Default.DomEvent);

        Assert.NotNull(evt);
        Assert.Equal("drop", evt.Type);
        Assert.Equal("bin", evt.TargetId);
        Assert.Equal("item-7", evt.DataTransfer);
    }

    [Fact]
    public void DataTransferIsNullWhenAbsent()
    {
        var evt = JsonSerializer.Deserialize("""{"type":"click"}""", DomEventJsonContext.Default.DomEvent);
        Assert.NotNull(evt);
        Assert.Null(evt.DataTransfer);
    }
}
