using Web;
using CC.CSX;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributeKeys;

var builder = WebApplication.CreateSlimBuilder(args);
var app = builder.Build();
var samples = TodoGenerator.GenerateTodos().ToArray();
var todos = app.MapGroup("/todos");
var Master = (HtmlNode node) => Html(Body(H1("Sample todos"), node));
var node = Li("test");
app.Run();