using System.Buffers;
using System.Text;

namespace CC.CSX;

/// <summary>
/// HTML-escapes text and attribute values. Uses a vectorized scan (<see cref="SearchValues{T}"/>)
/// with a verbatim fast path: when the value contains no special character (the common case) it is
/// written in one shot with no per-char work and no allocation. Escapes <c>&amp; &lt; &gt; " '</c>,
/// which is safe for both element text and double-quoted attribute values.
/// </summary>
public static class HtmlEscape
{
    static readonly SearchValues<char> Special = SearchValues.Create("&<>\"'");

    static string Entity(char c) => c switch
    {
        '&' => "&amp;",
        '<' => "&lt;",
        '>' => "&gt;",
        '"' => "&quot;",
        '\'' => "&#39;",
        _ => "",
    };

    /// <summary>Writes <paramref name="value"/> to <paramref name="tw"/>, HTML-escaped.</summary>
    public static void WriteEscaped(TextWriter tw, string? value)
    {
        if (!string.IsNullOrEmpty(value)) WriteEscaped(tw, value.AsSpan());
    }

    /// <summary>Writes <paramref name="s"/> to <paramref name="tw"/>, HTML-escaped.</summary>
    public static void WriteEscaped(TextWriter tw, ReadOnlySpan<char> s)
    {
        int idx = s.IndexOfAny(Special);
        if (idx < 0) { tw.Write(s); return; } // fast path: nothing to escape
        int start = 0;
        while (idx >= 0)
        {
            if (idx > start) tw.Write(s.Slice(start, idx - start));
            tw.Write(Entity(s[idx]));
            start = idx + 1;
            int next = s.Slice(start).IndexOfAny(Special);
            idx = next < 0 ? -1 : start + next;
        }
        if (start < s.Length) tw.Write(s.Slice(start));
    }

    /// <summary>Appends <paramref name="value"/> to <paramref name="sb"/>, HTML-escaped.</summary>
    public static void AppendEscaped(StringBuilder sb, string? value)
    {
        if (!string.IsNullOrEmpty(value)) AppendEscaped(sb, value.AsSpan());
    }

    /// <summary>Appends <paramref name="s"/> to <paramref name="sb"/>, HTML-escaped.</summary>
    public static void AppendEscaped(StringBuilder sb, ReadOnlySpan<char> s)
    {
        int idx = s.IndexOfAny(Special);
        if (idx < 0) { sb.Append(s); return; }
        int start = 0;
        while (idx >= 0)
        {
            if (idx > start) sb.Append(s.Slice(start, idx - start));
            sb.Append(Entity(s[idx]));
            start = idx + 1;
            int next = s.Slice(start).IndexOfAny(Special);
            idx = next < 0 ? -1 : start + next;
        }
        if (start < s.Length) sb.Append(s.Slice(start));
    }

    /// <summary>Returns <paramref name="value"/> HTML-escaped (or the same instance if nothing to escape).</summary>
    public static string Escape(string? value)
    {
        if (string.IsNullOrEmpty(value) || value!.AsSpan().IndexOfAny(Special) < 0) return value ?? string.Empty;
        var sb = new StringBuilder(value.Length + 16);
        AppendEscaped(sb, value.AsSpan());
        return sb.ToString();
    }
}
