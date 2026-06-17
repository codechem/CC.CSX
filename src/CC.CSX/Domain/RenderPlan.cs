using System.Buffers;
using System.Text;

namespace CC.CSX;

/// <summary>
/// A compiled view: an ordered sequence of static UTF-8 byte chunks and dynamic holes. Static runs
/// are baked once at <see cref="Compile"/> time and written by memcpy; only the holes
/// (<see cref="DynNode"/>) and loops (<see cref="EachNode{T}"/>) are evaluated per render.
/// </summary>
/// <remarks>
/// Plans are valid only at <see cref="RenderOptions.Indent"/> == 0 (indented output depends on
/// nesting depth) — compile and execute with Indent 0. A plan is immutable and safe to hold in a
/// <c>static readonly</c> field and share across threads; the hole callbacks read whatever state
/// they close over at execution time.
/// </remarks>
public sealed class RenderPlan
{
    private readonly RenderSegment[] _segments;

    private RenderPlan(RenderSegment[] segments) => _segments = segments;

    /// <summary>Number of segments (static + dynamic). Useful for tests/diagnostics.</summary>
    public int SegmentCount => _segments.Length;

    /// <summary>Executes the plan, streaming UTF-8 into <paramref name="output"/>.</summary>
    public void WriteTo(IBufferWriter<byte> output)
    {
        using var w = new Utf8HtmlWriter(output);
        TextWriter tw = w;
        foreach (RenderSegment seg in _segments) seg.WriteTo(w, ref tw);
    }

    /// <summary>Renders the plan to a string (decodes the UTF-8). Mainly for tests/diagnostics.</summary>
    public string Render()
    {
        var buf = new ArrayBufferWriter<byte>();
        WriteTo(buf);
        return Encoding.UTF8.GetString(buf.WrittenSpan);
    }

    /// <summary>
    /// Walks <paramref name="root"/> once, coalescing runs of static markup into baked byte segments
    /// and turning <see cref="DynNode"/>/<see cref="EachNode{T}"/> into hole/loop segments.
    /// </summary>
    public static RenderPlan Compile(HtmlNode root)
    {
        var segments = new List<RenderSegment>();
        var buf = new ArrayBufferWriter<byte>();
        var w = new Utf8HtmlWriter(buf);

        void FlushStatic()
        {
            w.Flush();
            if (buf.WrittenCount > 0)
            {
                segments.Add(new StaticSegment(buf.WrittenSpan.ToArray()));
                buf.Clear();
            }
        }

        void Emit(HtmlNode node)
        {
            switch (node)
            {
                case DynNode d:
                    FlushStatic();
                    segments.Add(new HoleSegment(d.Produce));
                    break;
                case IEachNode each:
                    FlushStatic();
                    segments.Add(new LoopSegment(each.Expand));
                    break;
                case Fragment frag:
                    if (frag.RawChildren is { } fc)
                        foreach (HtmlNode c in fc) Emit(c);
                    break;
                case HtmlTextNode or RawHtml or CachedFragment:
                    // opaque-but-static: render verbatim into the current static run
                    EmitStatic(node);
                    break;
                default:
                    // a regular element: open tag + attributes are static, children may cut the run
                    TextWriter tw = w;
                    w.Write('<');
                    w.Write(node.Name);
                    if (node.RawAttributes is { } attrs)
                        foreach (HtmlAttribute a in attrs) { w.Write(' '); a.WriteTo(ref tw); }
                    w.Write('>');
                    if (node.RawChildren is { } children)
                        foreach (HtmlNode c in children) Emit(c);
                    w.Write('<');
                    w.Write('/');
                    w.Write(node.Name);
                    w.Write('>');
                    break;
            }
        }

        void EmitStatic(HtmlNode node)
        {
            TextWriter tw = w;
            node.WriteTo(ref tw);
        }

        Emit(root);
        FlushStatic();
        w.Dispose();
        return new RenderPlan(segments.ToArray());
    }
}

/// <summary>One step of a <see cref="RenderPlan"/>.</summary>
internal abstract class RenderSegment
{
    public abstract void WriteTo(Utf8HtmlWriter w, ref TextWriter tw);
}

internal sealed class StaticSegment(byte[] utf8) : RenderSegment
{
    private readonly byte[] _utf8 = utf8;
    public override void WriteTo(Utf8HtmlWriter w, ref TextWriter tw) => w.WriteRawUtf8(_utf8);
}

internal sealed class HoleSegment(Func<HtmlNode> produce) : RenderSegment
{
    private readonly Func<HtmlNode> _produce = produce;
    public override void WriteTo(Utf8HtmlWriter w, ref TextWriter tw) => _produce().WriteTo(ref tw);
}

internal sealed class LoopSegment(Func<IEnumerable<HtmlNode>> expand) : RenderSegment
{
    private readonly Func<IEnumerable<HtmlNode>> _expand = expand;
    public override void WriteTo(Utf8HtmlWriter w, ref TextWriter tw)
    {
        foreach (HtmlNode n in _expand()) n.WriteTo(ref tw);
    }
}
