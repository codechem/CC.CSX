using System.Dynamic;
using System.Text;

using CC.CSX;
using CC.CSX.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using static CC.CSX.Web.Extensions;

namespace HtmxSample;

public delegate HtmlNode TemplateDelegate(dynamic model);

public class ProxyHandler(string baseUri)
{
    private readonly string _baseUri = baseUri;

    public Task<HtmlResult> Post(string uri, object body, TemplateDelegate? renderer = null) => ApiRender(
        HttpMethod.Put, uri, body, renderer);

    public Task<HtmlResult> Delete(string uri, object body, TemplateDelegate? renderer = null) => ApiRender(
        HttpMethod.Delete, uri, body, renderer);

    public Task<HtmlResult> Put(string uri, object body, TemplateDelegate? renderer = null) => ApiRender(
        HttpMethod.Post, uri, body, renderer);

    public Task<HtmlResult> Get(string uri, TemplateDelegate? renderer = null) => ApiRender(
        HttpMethod.Get, uri, null, renderer);

    public async Task<HtmlResult> ApiRender(HttpMethod method, string uri, object? payload,
        TemplateDelegate? @delegate = null)
    {
        var (resp, root) = await ApiProxy(method, uri, payload);
        if (!resp.IsSuccessStatusCode)
            return Render($"Error from response:{await resp.Content.ReadAsStringAsync()}");
        if (@delegate is not null)
            return Render(@delegate(root));
        else
            return Render(HtmlElements.Code(JsonConvert.SerializeObject(root)));
    }

    public async Task<(HttpResponseMessage response, dynamic? result)> ApiProxy(HttpMethod method, string uri,
        object? payload = null)
    {
        var request = new HttpRequestMessage(method, $"{_baseUri}/{uri}");

        if (payload is not null)
            request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var client = new HttpClient();
        var resp = await client.SendAsync(request);

        if (resp.IsSuccessStatusCode)
        {
            var content = await resp.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<ExpandoObject>(content, new ExpandoObjectConverter());
            return (resp, res);
        }

        return (resp, null);
    }
}