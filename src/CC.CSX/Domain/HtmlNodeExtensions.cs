namespace CC.CSX;

public static class HtmlNodeExtensions
{
    public static void ApplyWhen(this HtmlNode node,
            Func<HtmlNode, bool> condition,
            Action<HtmlNode> action)
        => node.When(condition).ApplyEach(action);

    public static void Apply(this HtmlNode node, Action<HtmlNode> action)
        => node.When(_ => true).ApplyEach(action);

    public static IEnumerable<HtmlNode> When(this HtmlNode node,
            Func<HtmlNode, bool> condition)
    {
        if (condition.Invoke(node))
            yield return node;
        foreach (var child in node.Children)
            foreach (var res in When(child, condition))
                yield return res;
    }

    public static void ApplyEach(this IEnumerable<HtmlNode> nodes,
            Action<HtmlNode> action)
    {
        foreach (var node in nodes)
            action(node);
    }
}