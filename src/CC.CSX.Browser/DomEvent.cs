using System.Text.Json.Serialization;

namespace CC.CSX.Browser;

/// <summary>
/// Snapshot of the DOM event that triggered a handler, marshaled from JS.
/// Fields that do not apply to the event type are null.
/// </summary>
public sealed record DomEvent
{
    /// <summary>DOM event type, e.g. "click", "input", "scroll".</summary>
    public string Type { get; init; } = "";
    /// <summary>Current value of the target form control, if it has one.</summary>
    public string? Value { get; init; }
    /// <summary>Checked state of the target checkbox/radio, if applicable.</summary>
    public bool? Checked { get; init; }
    /// <summary>Key name for keyboard events (e.g. "Enter").</summary>
    public string? Key { get; init; }
    /// <summary>Physical key code for keyboard events (e.g. "KeyA").</summary>
    public string? Code { get; init; }
    /// <summary>Mouse button index for mouse events.</summary>
    public int? Button { get; init; }
    public double? ClientX { get; init; }
    public double? ClientY { get; init; }
    public double? DeltaX { get; init; }
    public double? DeltaY { get; init; }
    public bool CtrlKey { get; init; }
    public bool ShiftKey { get; init; }
    public bool AltKey { get; init; }
    public bool MetaKey { get; init; }
    /// <summary>The id attribute of the event target, if any.</summary>
    public string? TargetId { get; init; }
    public double? ScrollTop { get; init; }
    public double? ScrollLeft { get; init; }
}

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(DomEvent))]
internal partial class DomEventJsonContext : JsonSerializerContext;
