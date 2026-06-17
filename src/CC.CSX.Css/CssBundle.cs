using System.Security.Cryptography;
using System.Text;

namespace CC.CSX.Css;

/// <summary>
/// A named piece of CSS baked into an assembly — typically emitted by the
/// CC.CSX.Css source generator from a <c>.css</c> file, but it can also be authored by hand.
/// Component libraries can expose their bundles so consuming apps compose all styles.
/// </summary>
public interface ICssBundle
{
    /// <summary>Logical name of the stylesheet (usually the file name without extension).</summary>
    string Name { get; }
    /// <summary>The CSS source text.</summary>
    string Content { get; }
    /// <summary>A short stable hash of <see cref="Content"/>, usable for cache-busting URLs.</summary>
    string ContentHash { get; }
}

/// <summary>Default <see cref="ICssBundle"/> implementation.</summary>
public sealed class CssBundle(string name, string content) : ICssBundle
{
    string? hash;

    /// <inheritdoc/>
    public string Name { get; } = name;
    /// <inheritdoc/>
    public string Content { get; } = content;
    /// <inheritdoc/>
    public string ContentHash => hash ??= ComputeHash(Content);

    static string ComputeHash(string content)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(content));
#if NET9_0_OR_GREATER
        return Convert.ToHexStringLower(bytes)[..12];
#else
        return Convert.ToHexString(bytes).ToLowerInvariant()[..12];
#endif
    }
}
