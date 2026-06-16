using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;

namespace CC.CSX.Browser;

/// <summary>
/// Client-side runtime for CC.CSX views. Mount a view function on a DOM
/// selector; every dispatched event re-renders the view and morphs the DOM
/// (preserving focus, caret and untouched subtrees).
/// </summary>
public static partial class BrowserApp
{
    const string ModuleName = "ccsx-browser";

    static Func<HtmlNode>? view;
    static string rootSelector = "#app";
    static bool mounted;
    // rooted so the JS-side function proxies never lose their .NET delegates
    static Action<string, string>? dispatchRef;
    static Action<string>? dispatchHxRef;

    /// <summary>
    /// Imports the JS glue module, renders the view into <paramref name="selector"/>
    /// and starts dispatching DOM events to .NET handlers.
    /// </summary>
    public static async Task MountAsync(string selector, Func<HtmlNode> viewFn)
    {
        view = viewFn;
        await InitCoreAsync(selector);
        Refresh();
    }

    /// <summary>
    /// Starts event dispatch and hx interception over the existing DOM under
    /// <paramref name="selector"/> without mounting a view — for pages whose
    /// initial HTML is static or server-rendered and driven purely by hx routes.
    /// </summary>
    public static Task StartAsync(string selector) => InitCoreAsync(selector);

    static async Task InitCoreAsync(string selector)
    {
        rootSelector = selector;
        await ImportRuntimeModuleAsync();
        dispatchRef = Dispatch;
        dispatchHxRef = DispatchHx;
        Init(selector, dispatchRef, dispatchHxRef);
        mounted = true;
    }

    /// <summary>Registers a stable named action (route-like, htmx-style) usable via <c>htAction("name")</c>.</summary>
    public static void Map(string action, Action handler) => EventRegistry.MapAction(action, handler);
    /// <inheritdoc cref="Map(string, Action)"/>
    public static void Map(string action, Action<DomEvent> handler) => EventRegistry.MapAction(action, handler);
    /// <inheritdoc cref="Map(string, Action)"/>
    public static void Map(string action, Func<Task> handler) => EventRegistry.MapAction(action, handler);
    /// <inheritdoc cref="Map(string, Action)"/>
    public static void Map(string action, Func<DomEvent, Task> handler) => EventRegistry.MapAction(action, handler);

    /// <summary>Keeps Main alive so the runtime can keep dispatching events.</summary>
    public static Task RunAsync() => Task.Delay(Timeout.Infinite);

    /// <summary>Re-renders the view and morphs the DOM. Call after out-of-band state changes.</summary>
    public static void Refresh()
    {
        if (view is null || !mounted) return;
        EventRegistry.BeginCycle();
        string html;
        try { html = view().ToString(); }
        finally { EventRegistry.CommitCycle(); }
        Morph(rootSelector, html);
        foreach (var eventName in EventRegistry.DrainPendingEvents())
            EnsureListener(eventName);
    }

    static void Dispatch(string handlerKey, string eventJson)
    {
        var handler = EventRegistry.Resolve(handlerKey);
        if (handler is null) return;
        var evt = JsonSerializer.Deserialize(eventJson, DomEventJsonContext.Default.DomEvent) ?? new DomEvent();
        switch (handler)
        {
            case Action a: a(); Refresh(); break;
            case Action<DomEvent> a: a(evt); Refresh(); break;
            case Func<Task> f: _ = InvokeAsync(f); break;
            case Func<DomEvent, Task> f: _ = InvokeAsync(() => f(evt)); break;
        }
    }

    static async Task InvokeAsync(Func<Task> invoke)
    {
        try { await invoke(); }
        finally { Refresh(); }
    }

    // The JS glue ships as an embedded resource and is imported as an ES module
    // through a data: URL, so no static web asset wiring is required of the host app.
    static async Task ImportRuntimeModuleAsync()
    {
        await using var stream = typeof(BrowserApp).Assembly.GetManifestResourceStream("CC.CSX.Browser.ccsx-browser.js")
            ?? throw new InvalidOperationException("embedded ccsx-browser.js resource is missing");
        using var reader = new StreamReader(stream);
        var js = await reader.ReadToEndAsync();
        var dataUrl = "data:text/javascript;base64," + Convert.ToBase64String(Encoding.UTF8.GetBytes(js));
        await JSHost.ImportAsync(ModuleName, dataUrl);
    }

    [JSImport("init", ModuleName)]
    static partial void Init(string selector,
        [JSMarshalAs<JSType.Function<JSType.String, JSType.String>>] Action<string, string> dispatch,
        [JSMarshalAs<JSType.Function<JSType.String>>] Action<string> dispatchHx);

    [JSImport("ensureListener", ModuleName)]
    static partial void EnsureListener(string eventName);

    [JSImport("morph", ModuleName)]
    static partial void Morph(string selector, string html);
}
