namespace CC.CSX.Browser;

/// <summary>
/// Holds the mapping between handler keys rendered into the DOM
/// (<c>data-ht-on-*</c> attribute values) and .NET delegates.
/// Delegate handlers are re-registered on every render cycle (their ids are
/// only valid for the markup produced by that cycle); named actions are
/// stable for the lifetime of the app, like routes.
/// </summary>
internal static class EventRegistry
{
    static Dictionary<int, Delegate> live = [];
    static Dictionary<int, Delegate>? building;
    static readonly Dictionary<string, Delegate> actions = new(StringComparer.Ordinal);
    static readonly HashSet<string> seenEvents = [];
    static readonly List<string> pendingEvents = [];
    static int nextId;

    /// <summary>Starts collecting handlers for a new render cycle.</summary>
    public static void BeginCycle() => building = [];

    /// <summary>Swaps the freshly built handler table in as the live one.</summary>
    public static void CommitCycle()
    {
        if (building is not null) live = building;
        building = null;
    }

    public static int Register(string eventName, Delegate handler)
    {
        RequireEvent(eventName);
        var table = building ?? live;
        var id = ++nextId;
        table[id] = handler;
        return id;
    }

    public static void MapAction(string name, Delegate handler) => actions[name] = handler;

    /// <summary>Marks a DOM event type as used so a root listener gets attached for it.</summary>
    public static void RequireEvent(string eventName)
    {
        if (seenEvents.Add(eventName)) pendingEvents.Add(eventName);
    }

    /// <summary>Returns event types that still need a JS listener and clears the queue.</summary>
    public static string[] DrainPendingEvents()
    {
        if (pendingEvents.Count == 0) return [];
        var copy = pendingEvents.ToArray();
        pendingEvents.Clear();
        return copy;
    }

    /// <summary>Numeric keys are per-render delegate ids, anything else is a named action.</summary>
    public static Delegate? Resolve(string key) =>
        int.TryParse(key, out var id)
            ? live.GetValueOrDefault(id)
            : actions.GetValueOrDefault(key);
}
