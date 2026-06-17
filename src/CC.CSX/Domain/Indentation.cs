using System.Text;

namespace CC.CSX;

/// <summary>
/// Non-allocating indentation writers. Indentation is emitted from a single cached buffer
/// of spaces instead of allocating a fresh <c>new string(' ', n)</c> for every node on every
/// render (the default <see cref="RenderOptions.Indent"/> is &gt; 0, so this is a per-node cost).
/// </summary>
internal static class Indentation
{
    const int CacheSize = 256;
    static readonly string Spaces = new(' ', CacheSize);

    /// <summary>Writes <paramref name="count"/> spaces to <paramref name="tw"/> without allocating.</summary>
    public static void WriteTo(TextWriter tw, int count)
    {
        while (count > 0)
        {
            int n = count < CacheSize ? count : CacheSize;
            tw.Write(Spaces.AsSpan(0, n)); // TextWriter.Write(ReadOnlySpan<char>) — no string allocation
            count -= n;
        }
    }

    /// <summary>Appends <paramref name="count"/> spaces to <paramref name="sb"/> without allocating.</summary>
    public static void AppendTo(StringBuilder sb, int count) => sb.Append(' ', count);
}
