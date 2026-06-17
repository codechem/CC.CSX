using System.Collections.Concurrent;
using System.Text;

namespace CC.CSX;

/// <summary>
/// Process-wide control + store for cached HTML fragments. Caching renders a static subtree once
/// and reuses the bytes, skipping the rebuild + re-render of unchanging chrome (head, nav, footer)
/// on every request.
/// </summary>
/// <remarks>
/// <see cref="Enabled"/> defaults to <c>true</c> in Release builds and <c>false</c> in Debug, so
/// development always sees fresh output (edits show immediately) while production gets the win.
/// It is a settable flag, so either default can be overridden at startup. Only cache fragments that
/// are genuinely static for their key — a fragment holding per-request data would serve stale content.
/// </remarks>
public static class FragmentCache
{
    /// <summary>
    /// Whether fragment caching takes effect. Default: enabled in Release, disabled in Debug.
    /// </summary>
    public static bool Enabled { get; set; } =
#if DEBUG
        false;
#else
        true;
#endif

    private static readonly ConcurrentDictionary<string, CachedFragment> Store = new();

    /// <summary>
    /// Returns a cached fragment for <paramref name="key"/>, building it with <paramref name="factory"/>
    /// the first time. When caching is disabled the fragment is built fresh and not stored.
    /// </summary>
    public static HtmlNode GetOrAdd(string key, Func<HtmlNode> factory)
        => Enabled
            ? Store.GetOrAdd(key, _ => new CachedFragment(factory()))
            : new CachedFragment(factory());

    /// <summary>Empties the keyed fragment store.</summary>
    public static void Clear() => Store.Clear();
}

/// <summary>
/// Wraps a source node and, when caching is in effect, renders it once and emits the cached result
/// thereafter. Decides per render (not at construction) so the <see cref="FragmentCache.Enabled"/>
/// flag is honored dynamically. Caching only kicks in when <see cref="RenderOptions.Indent"/> is 0;
/// with indentation the output depends on nesting depth, so the source is rendered live instead.
/// </summary>
public sealed class CachedFragment : HtmlNode
{
    private const string CachedKey = "#cached";
    private readonly HtmlNode _source;
    private RawHtml? _rendered;

    /// <summary>Wraps <paramref name="source"/> for (flag-gated) caching.</summary>
    public CachedFragment(HtmlNode source) : base(CachedKey)
        => _source = source ?? throw new ArgumentNullException(nameof(source));

    private bool UseCache => FragmentCache.Enabled && RenderOptions.Indent == 0;

    // Rendered at Indent == 0 (the only state in which the cache is used), so it is reusable at any depth.
    private RawHtml Rendered => _rendered ??= new RawHtml(_source.ToString());

    /// <inheritdoc/>
    public override string ToString() => UseCache ? Rendered.ToString() : _source.ToString();

    /// <inheritdoc/>
    public override string ToString(int indent = 0) => UseCache ? Rendered.ToString(indent) : _source.ToString(indent);

    /// <inheritdoc/>
    public override void AppendTo(ref StringBuilder sb, int indent = 0)
    {
        if (UseCache) Rendered.AppendTo(ref sb, indent);
        else _source.AppendTo(ref sb, indent);
    }

    /// <inheritdoc/>
    public override void WriteTo(ref TextWriter tw, int indent = 0)
    {
        if (UseCache)
        {
            RawHtml r = Rendered;
            r.WriteTo(ref tw, indent);
        }
        else
        {
            _source.WriteTo(ref tw, indent);
        }
    }
}
