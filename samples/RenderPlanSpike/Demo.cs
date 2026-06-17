using CC.CSX;

namespace RenderPlanSpike;

// Call sites of the [RenderOptimized] views. Because this project enables interceptors for the
// CC.CSX.Generated namespace, the generator rewrites these `Views.X(...)` calls to the optimized
// builder — so each returns a PlanNode instead of building a real HtmlNode tree.
public static class Demo
{
    public static HtmlNode UserRow() => Views.UserRow(1, "Ann", "ann@example.com");

    public static HtmlNode Status(bool ok) => Views.Status(ok);

    public static HtmlNode Report(IEnumerable<(int id, string name, string email)> rows) => Views.Report(rows);
}
