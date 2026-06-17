using System;
using System.Collections.Generic;
using System.Linq;
using CC.CSX;
using CC.CSX.Browser;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;
using static CC.CSX.Browser.HtmlEvents;

var rng = new Random();

string[] ranges = ["Today", "Last 7 days", "Last 30 days"];
string range = "Last 7 days";

string statusFilter = "All";
string[] statuses = ["All", "Paid", "Pending", "Refunded"];

int amountSort = 0; // 0 = none, 1 = asc, -1 = desc

string[] customers =
[
    "Acme Corp", "Globex", "Initech", "Umbrella", "Hooli",
    "Stark Industries", "Wayne Enterprises", "Wonka Inc", "Cyberdyne",
    "Soylent", "Tyrell Corp", "Aperture",
];
string[] orderStatuses = ["Paid", "Pending", "Refunded"];

Kpi[] kpis = [];
Bar[] chart = [];
Order[] orders = [];

ReseedAll();

await BrowserApp.MountAsync("#app", View);
await BrowserApp.RunAsync();

void ReseedAll()
{
    SeedKpis();
    SeedChart();
    SeedOrders();
}

void SeedKpis()
{
    double revFactor = range switch { "Today" => 0.18, "Last 30 days" => 4.3, _ => 1.0 };
    double countFactor = range switch { "Today" => 0.2, "Last 30 days" => 4.1, _ => 1.0 };

    int revenue = (int)(rng.Next(38000, 56000) * revFactor);
    int ordersCount = (int)(rng.Next(820, 1240) * countFactor);
    int visitors = (int)(rng.Next(12000, 24000) * countFactor);
    double conversion = Math.Round(rng.NextDouble() * 3 + 2, 1);

    kpis =
    [
        new("Revenue", $"${revenue:N0}", Delta()),
        new("Orders", $"{ordersCount:N0}", Delta()),
        new("Visitors", $"{visitors:N0}", Delta()),
        new("Conversion", $"{conversion:0.0}%", Delta()),
    ];

    (bool Up, string Text) Delta()
    {
        bool up = rng.Next(2) == 0;
        double pct = Math.Round(rng.NextDouble() * 18 + 0.5, 1);
        return (up, $"{(up ? "▲" : "▼")} {pct:0.0}%");
    }
}

void SeedChart()
{
    string[] labels = range switch
    {
        "Today" => ["00", "04", "08", "12", "16", "20"],
        "Last 30 days" => ["Wk 1", "Wk 2", "Wk 3", "Wk 4"],
        _ => ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"],
    };
    int max = range switch { "Today" => 1200, "Last 30 days" => 90000, _ => 14000 };
    int min = max / 4;
    chart = labels.Select(l => new Bar(l, rng.Next(min, max))).ToArray();
}

void SeedOrders()
{
    int baseId = rng.Next(10200, 10800);
    orders = Enumerable.Range(0, 10).Select(i => new Order(
        baseId + i,
        customers[rng.Next(customers.Length)],
        orderStatuses[rng.Next(orderStatuses.Length)],
        rng.Next(45, 4800))).ToArray();
}

HtmlNode View()
{
    int chartMax = chart.Length == 0 ? 1 : chart.Max(b => b.Value);

    IEnumerable<Order> filtered = orders
        .Where(o => statusFilter == "All" || o.Status == statusFilter);
    if (amountSort != 0)
        filtered = amountSort > 0
            ? filtered.OrderBy(o => o.Amount)
            : filtered.OrderByDescending(o => o.Amount);
    var rows = filtered.ToArray();

    return Div(@class("app"),
        TopBar(),
        KpiRow(),
        ChartSection(chartMax),
        OrdersSection(rows));
}

HtmlNode TopBar() =>
    Div(@class("topbar"),
        Div(@class("brand"),
            Span(@class("dot")),
            H1("Pulse"),
            Small("Analytics overview")),
        Div(@class("controls"),
            Select(@class("select"), value(range),
                onChange(e => { range = e.Value!; ReseedAll(); }),
                ranges.Select(r => Option(value(r), r)).ToArray()),
            Button(@class("btn", "btn-primary"),
                onClick(() => ReseedAll()),
                "Refresh")));

HtmlNode KpiRow() =>
    Div(@class("kpi-grid"),
        kpis.Select(k => Div(@class("card", "kpi"),
            P(@class("kpi-label"), k.Label),
            P(@class("kpi-value"), k.Value),
            Span(@class(k.Delta.Up ? "kpi-delta up" : "kpi-delta down"), k.Delta.Text)))
            .ToArray());

HtmlNode ChartSection(int chartMax) =>
    Div(@class("section"),
        Div(@class("section-head"),
            H2(@class("section-title"), "Revenue over time")),
        Div(@class("card"),
            Div(@class("chart"),
                chart.Select(b => Div(@class("bar-col"),
                    Div(@class("bar"), style($"height:{Math.Max(2, b.Value * 100 / chartMax)}%")),
                    Span(@class("bar-label"), b.Label)))
                    .ToArray())));

HtmlNode OrdersSection(Order[] rows) =>
    Div(@class("section"),
        Div(@class("section-head"),
            H2(@class("section-title"), "Recent orders"),
            Div(@class("filter-tabs"),
                statuses.Select(s => Button(
                    @class(statusFilter == s ? "filter-tab active" : "filter-tab"),
                    onClick(() => statusFilter = s),
                    s)).ToArray())),
        Div(@class("card"),
            Table(@class("table"),
                Thead(Tr(
                    Th("Order"),
                    Th("Customer"),
                    Th("Status"),
                    Th(@class("sortable num"),
                        onClick(() => amountSort = amountSort == 1 ? -1 : 1),
                        "Amount",
                        amountSort == 0
                            ? (HtmlItem)HtmlNone.Instance
                            : Span(@class("arrow"), amountSort > 0 ? "▲" : "▼")))),
                Tbody(OrdersBody(rows)))));

HtmlNode OrdersBody(Order[] rows) =>
    rows.Length == 0
        ? Tr(Td(("colspan", "4"), @class("empty"), "No orders"))
        : (HtmlNode)rows.Select(o => Tr(
            Td(@class("mono"), $"#{o.Id}"),
            Td(o.Customer),
            Td(Span(@class($"badge {o.Status.ToLower()}"), o.Status)),
            Td(@class("num"), $"${o.Amount:N0}"))).ToArray();

record Kpi(string Label, string Value, (bool Up, string Text) Delta);
record Bar(string Label, int Value);
record Order(int Id, string Customer, string Status, int Amount);
