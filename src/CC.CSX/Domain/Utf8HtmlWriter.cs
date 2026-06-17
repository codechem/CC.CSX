using System.Buffers;
using System.Text;

namespace CC.CSX;

/// <summary>
/// A <see cref="TextWriter"/> that encodes its output as UTF-8 straight into an
/// <see cref="IBufferWriter{T}"/> of <see cref="byte"/> (e.g. an ASP.NET response
/// <c>PipeWriter</c>), using a single pooled char buffer.
/// <para>
/// This lets <see cref="HtmlNode.WriteTo(ref TextWriter, int)"/> stream a document in fixed-size
/// chunks instead of accumulating the whole render in one growing <see cref="StringBuilder"/>/string —
/// which, for pages over ~85 KB, lands on the Large Object Heap and forces Gen2 collections.
/// Char data is batched, so the many tiny writes a render emits (tag chars, attribute names) are
/// coalesced before encoding.
/// </para>
/// </summary>
public sealed class Utf8HtmlWriter : TextWriter
{
    const int DefaultCharCapacity = 4096;

    readonly IBufferWriter<byte> _output;
    char[] _buffer;
    int _pos;

    /// <summary>Creates a writer that encodes into <paramref name="output"/>.</summary>
    /// <param name="output">Destination for the UTF-8 bytes.</param>
    /// <param name="charCapacity">Size of the pooled char batching buffer.</param>
    public Utf8HtmlWriter(IBufferWriter<byte> output, int charCapacity = DefaultCharCapacity)
    {
        _output = output ?? throw new ArgumentNullException(nameof(output));
        _buffer = ArrayPool<char>.Shared.Rent(Math.Max(charCapacity, 256));
    }

    /// <inheritdoc/>
    public override Encoding Encoding => Encoding.UTF8;

    /// <inheritdoc/>
    public override void Write(char value)
    {
        if (_pos == _buffer.Length) FlushBuffer();
        _buffer[_pos++] = value;
    }

    /// <inheritdoc/>
    public override void Write(string? value)
    {
        if (value is not null) Write(value.AsSpan());
    }

    /// <inheritdoc/>
    public override void Write(char[] buffer, int index, int count)
        => Write(buffer.AsSpan(index, count));

    /// <inheritdoc/>
    public override void Write(ReadOnlySpan<char> value)
    {
        // Bigger than the batch buffer: drain what we have, then encode directly.
        if (value.Length >= _buffer.Length)
        {
            FlushBuffer();
            Encode(value);
            return;
        }
        if (_pos + value.Length > _buffer.Length) FlushBuffer();
        value.CopyTo(_buffer.AsSpan(_pos));
        _pos += value.Length;
    }

    /// <summary>
    /// Writes already-encoded UTF-8 bytes straight to the underlying buffer (after draining any
    /// pending chars). Used by <see cref="RawHtml"/> to emit a cached fragment without re-encoding.
    /// </summary>
    public void WriteRawUtf8(ReadOnlySpan<byte> utf8)
    {
        FlushBuffer();
        _output.Write(utf8);
    }

    /// <inheritdoc/>
    public override void Flush() => FlushBuffer();

    void FlushBuffer()
    {
        if (_pos == 0) return;
        Encode(_buffer.AsSpan(0, _pos));
        _pos = 0;
    }

    void Encode(ReadOnlySpan<char> chars)
    {
        int max = Encoding.UTF8.GetMaxByteCount(chars.Length);
        Span<byte> dest = _output.GetSpan(max);
        int written = Encoding.UTF8.GetBytes(chars, dest);
        _output.Advance(written);
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (disposing && _buffer is not null)
        {
            FlushBuffer();
            ArrayPool<char>.Shared.Return(_buffer);
            _buffer = null!;
        }
        base.Dispose(disposing);
    }
}
