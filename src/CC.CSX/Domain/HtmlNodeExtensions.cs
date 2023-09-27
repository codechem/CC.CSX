namespace CC.CSX;

/// <summary>
/// A class that contains extension methods for <see cref="HtmlNode"/>.
/// </summary>
public static class HtmlNodeExtensions
{
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
        foreach (var child in node.Children)
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