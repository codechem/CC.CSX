using CC.CSX;
using static CC.CSX.Web.Extensions;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributes;
using static CC.CSX.Htmx.HtmxAttributes;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Render(CalendarPage()));
app.MapGet("/calendar/{year:int}/{month:int}", (int year, int month) => Render(CalendarContent(year, month)));

app.Run();

static HtmlNode CalendarPage()
{
    return Html(
        Head(
            Title("Calendar Sample - CC.CSX + HTMX"),
            Meta(charset("utf-8")),
            Meta(name("viewport"), content("width=device-width, initial-scale=1")),
            Script(src("https://unpkg.com/htmx.org@1.9.10")),
            Style(CalendarStyles())
        ),
        Body(
            Div(@class("container"),
                H1("Interactive Calendar", @class("title")),
                Div(id("calendar-container"),
                    CalendarContent(DateTime.Now.Year, DateTime.Now.Month)
                )
            )
        )
    );
}

static HtmlNode CalendarContent(int year, int month)
{
    var date = new DateTime(year, month, 1);
    var prevMonth = date.AddMonths(-1);
    var nextMonth = date.AddMonths(1);
    var daysInMonth = DateTime.DaysInMonth(year, month);
    var firstDayOfWeek = (int)date.DayOfWeek;
    
    return Div(@class("calendar"),
        Div(@class("calendar-header"),
            Button(@class("nav-btn"),
                hxGet($"/calendar/{prevMonth.Year}/{prevMonth.Month}"),
                hxTarget("#calendar-container"),
                "‹"
            ),
            H2($"{date:MMMM yyyy}", @class("month-year")),
            Button(@class("nav-btn"),
                hxGet($"/calendar/{nextMonth.Year}/{nextMonth.Month}"),
                hxTarget("#calendar-container"),
                "›"
            )
        ),
        Div(@class("calendar-grid"),
            // Day headers
            Div("Sun", @class("day-header")),
            Div("Mon", @class("day-header")),
            Div("Tue", @class("day-header")),
            Div("Wed", @class("day-header")),
            Div("Thu", @class("day-header")),
            Div("Fri", @class("day-header")),
            Div("Sat", @class("day-header")),
            
            // Empty cells for days before the first of the month
            Enumerable.Range(0, firstDayOfWeek).Select(_ => Div(@class("day empty"))).ToArray(),
            
            // Days of the month
            Enumerable.Range(1, daysInMonth).Select(day =>
            {
                var isToday = year == DateTime.Now.Year && month == DateTime.Now.Month && day == DateTime.Now.Day;
                var className = isToday ? "day today" : "day";
                return Div(day.ToString(), @class(className));
            }).ToArray()
        )
    );
}

static string CalendarStyles()
{
    return @"
        body {
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
            margin: 0;
            padding: 20px;
            background-color: #f5f5f5;
        }
        
        .container {
            max-width: 800px;
            margin: 0 auto;
            background: white;
            border-radius: 12px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            overflow: hidden;
        }
        
        .title {
            text-align: center;
            margin: 0;
            padding: 30px 20px;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            font-weight: 300;
        }
        
        .calendar {
            padding: 20px;
        }
        
        .calendar-header {
            display: flex;
            align-items: center;
            justify-content: space-between;
            margin-bottom: 20px;
            padding: 0 10px;
        }
        
        .nav-btn {
            background: #667eea;
            color: white;
            border: none;
            border-radius: 50%;
            width: 40px;
            height: 40px;
            font-size: 18px;
            cursor: pointer;
            transition: all 0.2s ease;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        
        .nav-btn:hover {
            background: #5a67d8;
            transform: scale(1.1);
        }
        
        .month-year {
            margin: 0;
            font-size: 24px;
            font-weight: 600;
            color: #2d3748;
        }
        
        .calendar-grid {
            display: grid;
            grid-template-columns: repeat(7, 1fr);
            gap: 1px;
            background-color: #e2e8f0;
            border: 1px solid #e2e8f0;
        }
        
        .day-header {
            background: #667eea;
            color: white;
            padding: 12px;
            text-align: center;
            font-weight: 600;
            font-size: 14px;
        }
        
        .day {
            background: white;
            padding: 12px;
            text-align: center;
            min-height: 40px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: 500;
            transition: background-color 0.2s ease;
        }
        
        .day:hover:not(.empty) {
            background: #edf2f7;
            cursor: pointer;
        }
        
        .day.empty {
            background: #f7fafc;
        }
        
        .day.today {
            background: #667eea;
            color: white;
            font-weight: bold;
        }
        
        .day.today:hover {
            background: #5a67d8;
        }
    ";
}