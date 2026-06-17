namespace CC.CSX;

using System.Buffers;

/// <summary>
/// A class that contains extension methods for <see cref="HtmlNode"/>.
/// </summary>
public static class HtmlNodeExtensions
{
    /// <summary>
    /// Renders the node as UTF-8 directly into <paramref name="output"/> (e.g. an ASP.NET
    /// response <c>PipeWriter</c>), streaming in pooled chunks rather than building one large
    /// string. This is the lowest-allocation render path; prefer it over <c>ToString()</c> for
    /// large documents and high-throughput servers.
    /// </summary>
    public static void WriteTo(this HtmlItem node, IBufferWriter<byte> output, int indent = 0)
    {
        using var writer = new Utf8HtmlWriter(output);
        TextWriter tw = writer;
        node.WriteTo(ref tw, indent);
        // Utf8HtmlWriter.Dispose() flushes the batched chars into `output`.
    }

    /// <summary>
    /// Applies the given action to each node that satisfies the given condition.
    /// The nodes are searched in depth-first order.
    /// </summary>
    public static void ApplyWhen(this HtmlNode node,
            Func<HtmlNode, bool> condition,
            Action<HtmlNode> action)
        => node.When(condition).ApplyEach(action);

    /// <summary>
    /// Applies the given action to each node.
    /// The nodes are searched in depth-first order.
    /// </summary>
    public static void Apply(this HtmlNode node, Action<HtmlNode> action)
        => node.When(_ => true).ApplyEach(action);

    /// <summary>
    /// Returns <see cref="IEnumerable{T}"/> of nodes that satisfy the given condition.
    /// </summary>
    public static IEnumerable<HtmlNode> When(this HtmlNode node,
            Func<HtmlNode, bool> condition)
    {
        if (condition.Invoke(node))
            yield return node;
        if (node.RawChildren is not { } children) yield break;
        foreach (var child in children)
            foreach (var res in When(child, condition))
                yield return res;
    }

    /// <summary>
    /// Applies the given action to each node in the given collection.
    /// </summary>
    public static void ApplyEach(this IEnumerable<HtmlNode> nodes,
            Action<HtmlNode> action)
    {
        foreach (var node in nodes)
            action(node);
    }
}