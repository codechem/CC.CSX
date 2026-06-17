using System.Text;

namespace CC.CSX;

/// <summary>
/// A node that emits a pre-rendered HTML string verbatim (no escaping, no child walk).
/// Used as the cached representation of a fragment, and directly for trusted external HTML.
/// On the <see cref="Utf8HtmlWriter"/> path it writes its cached UTF-8 bytes (a memcpy) instead
/// of re-encoding the string each time.
/// </summary>
public sealed class RawHtml : HtmlNode
{
    private const string RawKey = "#raw";
    private readonly string _html;
    private byte[]? _utf8;

    /// <summary>Creates a node that renders <paramref name="html"/> exactly as given.</summary>
    public RawHtml(string html) : base(RawKey) => _html = html ?? string.Empty;

    private byte[] Utf8 => _utf8 ??= Encoding.UTF8.GetBytes(_html);

    /// <inheritdoc/>
    public override string ToString() => _html;

    /// <inheritdoc/>
    public override string ToString(int indent = 0) => _html;

    /// <inheritdoc/>
    public override void AppendTo(ref StringBuilder sb, int indent = 0) => sb.Append(_html);

    /// <inheritdoc/>
    public override void WriteTo(ref TextWriter tw, int indent = 0)
    {
        if (tw is Utf8HtmlWriter u) u.WriteRawUtf8(Utf8); // memcpy cached bytes
        else tw.Write(_html);
    }
}
