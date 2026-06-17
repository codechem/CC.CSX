using CC.CSX;

using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;

namespace RenderPlanSpike;

public record Product(string Name, decimal Price, bool InStock, bool OnSale, string[] Tags);

// A deliberately "dynamic-heavy" page: a loop of product cards, each with a value-level conditional
// class, a computed price string, a STRUCTURAL conditional (the SALE badge), and a NESTED loop
// (tags) — plus an inlined [HtmlPure] component. Exercises far more of the dynamic path than a
// plain data table, so it's a more realistic perf setup.
public static class CatalogViews
{
    [HtmlPure]
    public static HtmlNode Card(Product p) =>
        Div(@class(p.InStock ? "card in-stock" : "card out-of-stock"),
            H3(p.Name),
            P(@class("price"), $"${p.Price}"),
            p.OnSale ? Span(@class("badge"), "SALE") : None,
            Ul(@class("tags"), p.Tags.Select(t => Li(t)).ToArray()));

    [RenderOptimized]
    public static HtmlNode Catalog(IEnumerable<Product> products) =>
        Div(@class("catalog"),
            H1("Products"),
            Div(@class("grid"),
                products.Select(p => Card(p)).ToArray()));
}
