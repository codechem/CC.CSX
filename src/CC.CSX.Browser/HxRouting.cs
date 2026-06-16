namespace CC.CSX.Browser;

/// <summary>
/// An intercepted htmx-style request, handled in the browser instead of a server.
/// Params merge (later wins): query string, form/element values, route template captures.
/// </summary>
public sealed record HxRequest
{
    /// <summary>HTTP-style verb of the hx attribute: GET, POST, PUT, PATCH or DELETE.</summary>
    public required string Method { get; init; }
    /// <summary>Path portion of the hx attribute value (query string stripped).</summary>
    public required string Path { get; init; }
    public required IReadOnlyDictionary<string, string> Params { get; init; }
    /// <summary>The DOM event that triggered the request.</summary>
    public required DomEvent Event { get; init; }

    /// <summary>Convenience indexer over <see cref="Params"/>; null when absent.</summary>
    public string? this[string key] => Params.TryGetValue(key, out var v) ? v : null;
}

/// <summary>Wire format for hx dispatches coming from the JS glue.</summary>
internal sealed record HxPayload
{
    public int ReqId { get; init; }
    public string Method { get; init; } = "";
    public string Path { get; init; } = "";
    public Dictionary<string, string> Params { get; init; } = [];
    public DomEvent? Event { get; init; }
}

/// <summary>
/// Client-side route table for intercepted hx requests. Templates are
/// slash-separated with <c>{name}</c> capture segments (Minimal API style).
/// </summary>
internal static class HxRouter
{
    sealed record Route(string Method, string[] Segments, Delegate Handler);

    static readonly List<Route> routes = [];

    public static void Add(string method, string template, Delegate handler)
        => routes.Add(new Route(method, Split(SplitQuery(template).Path), handler));

    public static (Delegate Handler, Dictionary<string, string> PathParams)? Match(string method, string path)
    {
        var segments = Split(path);
        foreach (var route in routes)
        {
            if (route.Method != method || route.Segments.Length != segments.Length) continue;
            Dictionary<string, string>? captured = null;
            var ok = true;
            for (var i = 0; i < segments.Length; i++)
            {
                var pattern = route.Segments[i];
                if (pattern.Length > 1 && pattern[0] == '{' && pattern[^1] == '}')
                    (captured ??= [])[pattern[1..^1]] = Uri.UnescapeDataString(segments[i]);
                else if (!string.Equals(pattern, segments[i], StringComparison.Ordinal))
                {
                    ok = false;
                    break;
                }
            }
            if (ok) return (route.Handler, captured ?? []);
        }
        return null;
    }

    public static (string Path, Dictionary<string, string> Query) SplitQuery(string pathWithQuery)
    {
        var qi = pathWithQuery.IndexOf('?');
        if (qi < 0) return (pathWithQuery, []);
        var query = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var pair in pathWithQuery[(qi + 1)..].Split('&', StringSplitOptions.RemoveEmptyEntries))
        {
            var eq = pair.IndexOf('=');
            if (eq < 0) query[Uri.UnescapeDataString(pair)] = "";
            else query[Uri.UnescapeDataString(pair[..eq])] = Uri.UnescapeDataString(pair[(eq + 1)..]);
        }
        return (pathWithQuery[..qi], query);
    }

    internal static void Clear() => routes.Clear();

    static string[] Split(string path) => path.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
}
