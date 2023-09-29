using HtmxSample;

using static CC.CSX.Web.Extensions;
using static Views;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var jokeProxy = new ProxyHandler("https://official-joke-api.appspot.com/random_joke");
var boredApi = new ProxyHandler("https://www.boredapi.com/api/activity");

app.MapGet("/", () => Render(MainPage));
app.MapGet("/apis/bored/", () => boredApi.Get("", BoredItem));
app.MapGet("/apis/joke", () => jokeProxy.Get(string.Empty, JokeView));
app.Run();
