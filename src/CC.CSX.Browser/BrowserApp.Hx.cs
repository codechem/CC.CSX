using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;

namespace CC.CSX.Browser;

// htmx interception: elements carrying hx-get/hx-post/… (from CC.CSX.Htmx) are
// caught by the JS glue and routed to handlers registered here — the same view
// markup can run against a real server (htmx.js) or fully in-browser (WASM).
public static partial class BrowserApp
{
    /// <summary>Routes intercepted <c>hx-get</c> requests matching the template (supports <c>{param}</c> segments).</summary>
    public static void MapGet(string template, Func<HxRequest, HtmlItem?> handler) => HxRouter.Add("GET", template, handler);
    /// <inheritdoc cref="MapGet(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapGet(string template, Func<HtmlItem?> handler) => HxRouter.Add("GET", template, handler);
    /// <inheritdoc cref="MapGet(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapGet(string template, Func<HxRequest, Task<HtmlItem?>> handler) => HxRouter.Add("GET", template, handler);
    /// <inheritdoc cref="MapGet(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapGet(string template, Func<Task<HtmlItem?>> handler) => HxRouter.Add("GET", template, handler);

    /// <summary>Routes intercepted <c>hx-post</c> requests matching the template (supports <c>{param}</c> segments).</summary>
    public static void MapPost(string template, Func<HxRequest, HtmlItem?> handler) => HxRouter.Add("POST", template, handler);
    /// <inheritdoc cref="MapPost(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapPost(string template, Func<HtmlItem?> handler) => HxRouter.Add("POST", template, handler);
    /// <inheritdoc cref="MapPost(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapPost(string template, Func<HxRequest, Task<HtmlItem?>> handler) => HxRouter.Add("POST", template, handler);
    /// <inheritdoc cref="MapPost(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapPost(string template, Func<Task<HtmlItem?>> handler) => HxRouter.Add("POST", template, handler);

    /// <summary>Routes intercepted <c>hx-put</c> requests matching the template (supports <c>{param}</c> segments).</summary>
    public static void MapPut(string template, Func<HxRequest, HtmlItem?> handler) => HxRouter.Add("PUT", template, handler);
    /// <inheritdoc cref="MapPut(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapPut(string template, Func<HtmlItem?> handler) => HxRouter.Add("PUT", template, handler);
    /// <inheritdoc cref="MapPut(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapPut(string template, Func<HxRequest, Task<HtmlItem?>> handler) => HxRouter.Add("PUT", template, handler);
    /// <inheritdoc cref="MapPut(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapPut(string template, Func<Task<HtmlItem?>> handler) => HxRouter.Add("PUT", template, handler);

    /// <summary>Routes intercepted <c>hx-patch</c> requests matching the template (supports <c>{param}</c> segments).</summary>
    public static void MapPatch(string template, Func<HxRequest, HtmlItem?> handler) => HxRouter.Add("PATCH", template, handler);
    /// <inheritdoc cref="MapPatch(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapPatch(string template, Func<HtmlItem?> handler) => HxRouter.Add("PATCH", template, handler);
    /// <inheritdoc cref="MapPatch(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapPatch(string template, Func<HxRequest, Task<HtmlItem?>> handler) => HxRouter.Add("PATCH", template, handler);
    /// <inheritdoc cref="MapPatch(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapPatch(string template, Func<Task<HtmlItem?>> handler) => HxRouter.Add("PATCH", template, handler);

    /// <summary>Routes intercepted <c>hx-delete</c> requests matching the template (supports <c>{param}</c> segments).</summary>
    public static void MapDelete(string template, Func<HxRequest, HtmlItem?> handler) => HxRouter.Add("DELETE", template, handler);
    /// <inheritdoc cref="MapDelete(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapDelete(string template, Func<HtmlItem?> handler) => HxRouter.Add("DELETE", template, handler);
    /// <inheritdoc cref="MapDelete(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapDelete(string template, Func<HxRequest, Task<HtmlItem?>> handler) => HxRouter.Add("DELETE", template, handler);
    /// <inheritdoc cref="MapDelete(string, Func{HxRequest, HtmlItem?})"/>
    public static void MapDelete(string template, Func<Task<HtmlItem?>> handler) => HxRouter.Add("DELETE", template, handler);

    static void DispatchHx(string requestJson)
    {
        var payload = JsonSerializer.Deserialize(requestJson, DomEventJsonContext.Default.HxPayload);
        if (payload is null) return;
        _ = HandleHxAsync(payload);
    }

    static async Task HandleHxAsync(HxPayload payload)
    {
        var (path, query) = HxRouter.SplitQuery(payload.Path);
        var match = HxRouter.Match(payload.Method, path);
        if (match is null)
        {
            Console.Error.WriteLine($"CC.CSX.Browser: no client route for {payload.Method} {path}");
            HxComplete(payload.ReqId, "", false);
            return;
        }

        var merged = new Dictionary<string, string>(query, StringComparer.Ordinal);
        foreach (var (k, v) in payload.Params) merged[k] = v;
        foreach (var (k, v) in match.Value.PathParams) merged[k] = v;
        var request = new HxRequest
        {
            Method = payload.Method,
            Path = path,
            Params = merged,
            Event = payload.Event ?? new DomEvent(),
        };

        HtmlItem? result = null;
        try
        {
            result = match.Value.Handler switch
            {
                Func<HxRequest, HtmlItem?> h => h(request),
                Func<HtmlItem?> h => h(),
                Func<HxRequest, Task<HtmlItem?>> h => await h(request),
                Func<Task<HtmlItem?>> h => await h(),
                _ => null,
            };
        }
        finally
        {
            HxComplete(payload.ReqId, result?.ToString() ?? "", result is not null);
            foreach (var eventName in EventRegistry.DrainPendingEvents())
                EnsureListener(eventName);
        }
    }

    [JSImport("hxComplete", ModuleName)]
    static partial void HxComplete(int reqId, string html, bool hasContent);
}
