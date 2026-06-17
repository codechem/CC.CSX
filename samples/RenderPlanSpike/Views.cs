using CC.CSX;

using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;

namespace RenderPlanSpike;

// Plain views — NO Dyn/Each markers. The generator infers static vs dynamic purely from the fact
// that the CC.CSX factories are pure: a factory call with static args is static; an arg that reads
// a parameter is a hole. Build this project and read samples/RenderPlanSpike/generated/.../*.g.cs.
public static class Views
{
    // flat view: static <tr>/<td> scaffold, dynamic class + 3 cell values
    [RenderOptimized]
    public static HtmlNode UserRow(int id, string name, string email) =>
        Tr(@class(id % 2 == 0 ? "even" : "odd"),
            Td(id),
            Td("Name:"),      // literal -> static
            Td(name),
            Td(email));

    // all-static subtree: should collapse to a single baked segment
    [RenderOptimized]
    public static HtmlNode TableHeader() =>
        Thead(Tr(Th("Id"), Th("Name"), Th("Email")));

    // a loop: static chrome baked, the Select body decomposed per item
    [RenderOptimized]
    public static HtmlNode Report(IEnumerable<(int id, string name, string email)> rows) =>
        Div(@class("uk-container"),
            H1("Report"),
            Table(@class("uk-table"),
                Thead(Tr(Th("Id"), Th("Name"), Th("Email"))),
                Tbody(rows.Select(r => Tr(@class(r.id % 2 == 0 ? "even" : "odd"),
                    Td(r.id),
                    Td(r.name),
                    Td(r.email))).ToArray())));

    // inlining: Page calls another pure component (UserRow) — the generator should recurse into it
    [HtmlPure]
    public static HtmlNode Badge(string text) => Span(@class("badge"), text);

    [RenderOptimized]
    public static HtmlNode Profile(string userName) =>
        Div(@class("profile"),
            H1(userName),
            Badge("online"),          // pure, all-static arg -> fully static
            Badge(userName));         // pure, dynamic arg -> static span scaffold + hole

    // structural conditional: the branches produce different subtrees -> per-branch sub-plans
    [RenderOptimized]
    public static HtmlNode Status(bool ok) =>
        Div(@class("status"),
            ok ? Span(@class("ok"), "Online")
               : Span(@class("err"), "Offline"));

    // escaping: a constant with special chars (baked escaped) + dynamic text and attribute holes
    // (escaped at runtime) — must match the live renderer exactly.
    [RenderOptimized]
    public static HtmlNode Escaped(string user, string note) =>
        Div(@class("box"),
            P("a < b & \"c\""),     // constant special chars
            P(user),                 // dynamic text hole
            Span(title(note), "ok")); // dynamic attribute-value hole

    // a boundary: calls an unknown/impure helper -> should become an opaque hole
    [RenderOptimized]
    public static HtmlNode WithUnknown(string s) =>
        Div(P(s), P(System.DateTime.Now.ToString()));
}
