namespace CC.CSX.Css.Generator;

/// <summary>
/// Minimal CSS scanner that extracts class-selector names from a stylesheet without a full
/// CSS parser. Skips comments, quoted strings and unquoted <c>url(...)</c> tokens, then
/// collects every <c>.identifier</c> token (CSS nesting friendly: classes are picked up at
/// any depth).
/// </summary>
public static class CssScanner
{
    /// <summary>Returns distinct class names in order of first appearance.</summary>
    public static IReadOnlyList<string> ExtractClassNames(string css)
    {
        var result = new List<string>();
        var seen = new HashSet<string>(StringComparer.Ordinal);
        int i = 0, n = css.Length;

        while (i < n)
        {
            char c = css[i];

            if (c == '/' && i + 1 < n && css[i + 1] == '*')
            {
                int end = css.IndexOf("*/", i + 2, StringComparison.Ordinal);
                i = end < 0 ? n : end + 2;
            }
            else if (c == '"' || c == '\'')
            {
                i = SkipString(css, i);
            }
            else if ((c == 'u' || c == 'U') && IsUrlOpen(css, i))
            {
                i = SkipUrl(css, i + 4);
            }
            else if (c == '.')
            {
                int j = ReadIdentifier(css, i + 1);
                if (j > i + 1)
                {
                    var name = css.Substring(i + 1, j - (i + 1));
                    if (seen.Add(name))
                        result.Add(name);
                    i = j;
                    continue;
                }
                i++;
            }
            else
            {
                i++;
            }
        }
        return result;
    }

    /// <summary>
    /// Converts a CSS class name to a lowercase-first C# identifier
    /// (<c>card--highlighted</c> → <c>cardHighlighted</c>).
    /// </summary>
    public static string ToCSharpIdentifier(string cssName)
    {
        var sb = new System.Text.StringBuilder(cssName.Length);
        bool upperNext = false;
        foreach (char c in cssName)
        {
            if (c is '-' or '_')
            {
                upperNext = sb.Length > 0;
                continue;
            }
            if (sb.Length == 0)
                sb.Append(char.ToLowerInvariant(c));
            else
                sb.Append(upperNext ? char.ToUpperInvariant(c) : c);
            upperNext = false;
        }
        if (sb.Length == 0)
            return "_";
        if (char.IsDigit(sb[0]))
            sb.Insert(0, '_');
        return sb.ToString();
    }

    static int SkipString(string css, int i)
    {
        char quote = css[i++];
        while (i < css.Length)
        {
            if (css[i] == '\\') i += 2;
            else if (css[i] == quote) return i + 1;
            else i++;
        }
        return i;
    }

    static bool IsUrlOpen(string css, int i)
    {
        if (i + 4 > css.Length) return false;
        if (i > 0 && (char.IsLetterOrDigit(css[i - 1]) || css[i - 1] is '-' or '_')) return false;
        return string.Compare(css, i, "url(", 0, 4, StringComparison.OrdinalIgnoreCase) == 0;
    }

    static int SkipUrl(string css, int i)
    {
        while (i < css.Length)
        {
            char c = css[i];
            if (c == '"' || c == '\'') i = SkipString(css, i);
            else if (c == ')') return i + 1;
            else i++;
        }
        return i;
    }

    static int ReadIdentifier(string css, int start)
    {
        int i = start;
        if (i < css.Length && css[i] == '-') i++;
        if (i >= css.Length || !(char.IsLetter(css[i]) || css[i] == '_'))
            return start;
        while (i < css.Length && (char.IsLetterOrDigit(css[i]) || css[i] is '-' or '_'))
            i++;
        return i;
    }
}
