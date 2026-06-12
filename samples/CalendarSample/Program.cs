using CC.CSX;
using CC.CSX.Css;

using static CC.CSX.Web.Extensions;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;
using static CC.CSX.Htmx.HtmxAttributes;
using static CalendarSample.Css; // typed classes generated from styles/calendar.css

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Render(CalendarPage()));
app.MapGet("/calendar/{year:int}/{month:int}", (int year, int month) => Render(CalendarContent(year, month)));

app.Run();

static HtmlNode CalendarPage() => Html(
    Head(
        Title("Calendar Sample - CC.CSX + HTMX"),
        Meta(charset("utf-8")),
        Meta(name("viewport"), content("width=device-width, initial-scale=1")),
        Script(src("https://unpkg.com/htmx.org@1.9.10")),
        CssImports.Inline(Calendar.Bundle)
    ),
    Body(
        Div(@class(Calendar.container),
            H1(@class(Calendar.title), "Interactive Calendar"),
            Div(id("calendar-container"),
                CalendarContent(DateTime.Now.Year, DateTime.Now.Month))
        )
    )
);

static HtmlNode CalendarContent(int year, int month)
{
    var date = new DateTime(year, month, 1);
    var today = DateTime.Now;

    return Div(@class(Calendar.calendar),
        Div(@class(Calendar.calendarHeader),
            NavButton("‹", date.AddMonths(-1)),
            H2(@class(Calendar.monthYear), $"{date:MMMM yyyy}"),
            NavButton("›", date.AddMonths(1))
        ),
        Div(@class(Calendar.calendarGrid),
            (from dayName in (string[])["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"]
             select Div(@class(Calendar.dayHeader), dayName)).ToArray(),

            (from _ in Enumerable.Range(0, (int)date.DayOfWeek)
             select Div(@class(Calendar.day + Calendar.empty))).ToArray(),

            (from day in Enumerable.Range(1, DateTime.DaysInMonth(year, month))
             let isToday = (year, month, day) == (today.Year, today.Month, today.Day)
             select Div(@class(isToday ? Calendar.day + Calendar.today : Calendar.day), day)).ToArray()
        )
    );
}

static HtmlNode NavButton(string label, DateTime target) =>
    Button(@class(Calendar.navBtn),
        hxGet($"/calendar/{target.Year}/{target.Month}"),
        hxTarget("#calendar-container"),
        label);
