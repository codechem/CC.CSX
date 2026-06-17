using System.Text;

namespace CC.CSX;

/// <summary>
/// Marks a dynamic hole in a view. When a <see cref="RenderPlan"/> is compiled, the surrounding
/// static markup is baked into byte segments and this node becomes a hole that is evaluated per
/// render. When rendered directly (no plan), it just produces and renders its node — so the same
/// view works with or without plan compilation.
/// </summary>
public sealed class DynNode : HtmlNode
{
    internal Func<HtmlNode> Produce { get; }

    /// <summary>Creates a dynamic hole that produces its node on each render.</summary>
    public DynNode(Func<HtmlNode> produce) : base("#dyn")
        => Produce = produce ?? throw new ArgumentNullException(nameof(produce));

    /// <inheritdoc/>
    public override string ToString() => Produce().ToString();
    /// <inheritdoc/>
    public override string ToString(int indent = 0) => Produce().ToString(indent);
    /// <inheritdoc/>
    public override void AppendTo(ref StringBuilder sb, int indent = 0) => Produce().AppendTo(ref sb, indent);
    /// <inheritdoc/>
    public override void WriteTo(ref TextWriter tw, int indent = 0) => Produce().WriteTo(ref tw, indent);
}

/// <summary>Non-generic view over an <see cref="EachNode{T}"/> so a <see cref="RenderPlan"/> can expand it.</summary>
internal interface IEachNode
{
    IEnumerable<HtmlNode> Expand();
}

/// <summary>
/// Marks a repeated dynamic region: renders <paramref name="renderItem"/> for each item. Compiles to
/// a loop hole in a <see cref="RenderPlan"/>; renders its items inline when used without a plan.
/// </summary>
public sealed class EachNode<T> : HtmlNode, IEachNode
{
    internal IEnumerable<T> Items { get; }
    internal Func<T, HtmlNode> RenderItem { get; }

    /// <summary>Creates a repeated region over <paramref name="items"/>.</summary>
    public EachNode(IEnumerable<T> items, Func<T, HtmlNode> renderItem) : base("#each")
    {
        Items = items ?? throw new ArgumentNullException(nameof(items));
        RenderItem = renderItem ?? throw new ArgumentNullException(nameof(renderItem));
    }

    /// <summary>Yields the rendered node for each item (re-evaluated on each call).</summary>
    public IEnumerable<HtmlNode> Expand()
    {
        foreach (T item in Items) yield return RenderItem(item);
    }

    /// <inheritdoc/>
    public override string ToString() => ToString(0);
    /// <inheritdoc/>
    public override string ToString(int indent = 0)
    {
        var sb = new StringBuilder();
        AppendTo(ref sb, indent);
        return sb.ToString();
    }
    /// <inheritdoc/>
    public override void AppendTo(ref StringBuilder sb, int indent = 0)
    {
        foreach (HtmlNode n in Expand()) n.AppendTo(ref sb, indent);
    }
    /// <inheritdoc/>
    public override void WriteTo(ref TextWriter tw, int indent = 0)
    {
        foreach (HtmlNode n in Expand()) n.WriteTo(ref tw, indent);
    }
}
