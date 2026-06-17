using System.Text;

namespace CC.CSX;

/// <summary>
/// A lightweight node returned by generated (render-plan) code. It captures the view's arguments in
/// a write callback instead of building an <see cref="HtmlNode"/> tree; the callback runs the baked
/// static byte segments + dynamic holes when the node is rendered. Cheap to create (one delegate),
/// so an intercepted view call allocates almost nothing until it is written.
/// </summary>
public sealed class PlanNode : HtmlNode
{
    private readonly Action<TextWriter> _write;

    /// <summary>Creates a plan node whose render is <paramref name="write"/>.</summary>
    public PlanNode(Action<TextWriter> write) : base("#plan")
        => _write = write ?? throw new ArgumentNullException(nameof(write));

    /// <inheritdoc/>
    public override void WriteTo(ref TextWriter tw, int indent = 0) => _write(tw);

    /// <inheritdoc/>
    public override void AppendTo(ref StringBuilder sb, int indent = 0)
    {
        TextWriter tw = new StringWriter(sb);
        _write(tw);
    }

    /// <inheritdoc/>
    public override string ToString(int indent = 0)
    {
        var sb = new StringBuilder();
        TextWriter tw = new StringWriter(sb);
        _write(tw);
        return sb.ToString();
    }

    /// <inheritdoc/>
    public override string ToString() => ToString(0);
}

/// <summary>Helpers used by generated render-plan code.</summary>
public static class PlanStatics
{
    /// <summary>
    /// Writes a baked static segment: the UTF-8 bytes via <see cref="Utf8HtmlWriter.WriteRawUtf8"/>
    /// (memcpy) when possible, otherwise the equivalent text to any other writer.
    /// </summary>
    public static void WriteStatic(TextWriter tw, byte[] utf8, string text)
    {
        if (tw is Utf8HtmlWriter u) u.WriteRawUtf8(utf8);
        else tw.Write(text);
    }
}
