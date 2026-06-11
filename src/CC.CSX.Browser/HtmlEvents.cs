namespace CC.CSX.Browser;

/// <summary>
/// Delegate-based event attributes for client-side (WASM) rendering.
/// Import with <c>using static CC.CSX.Browser.HtmlEvents;</c> — the typed
/// per-event overloads (<c>onClick(Action)</c>, …) live in HtmlEvents.g.cs.
/// </summary>
public static partial class HtmlEvents
{
    /// <summary>Handles an arbitrary DOM event (e.g. "pointerdown", "touchstart") with a .NET handler.</summary>
    public static HtmlAttribute on(string eventName, Action handler) => Bind(eventName, handler);
    /// <inheritdoc cref="on(string, Action)"/>
    public static HtmlAttribute on(string eventName, Action<DomEvent> handler) => Bind(eventName, handler);
    /// <inheritdoc cref="on(string, Action)"/>
    public static HtmlAttribute on(string eventName, Func<Task> handler) => Bind(eventName, handler);
    /// <inheritdoc cref="on(string, Action)"/>
    public static HtmlAttribute on(string eventName, Func<DomEvent, Task> handler) => Bind(eventName, handler);

    /// <summary>
    /// Binds the element to a stable named action registered with
    /// <see cref="BrowserApp.Map(string, Action)"/> — route-like, htmx-style.
    /// </summary>
    public static HtmlAttribute htAction(string action, string eventName = "click")
    {
        EventRegistry.RequireEvent(eventName);
        return new HtmlAttribute($"data-ht-on-{eventName}", action);
    }

    internal static HtmlAttribute Bind(string eventName, Delegate handler)
        => new($"data-ht-on-{eventName}", EventRegistry.Register(eventName, handler).ToString());
}
