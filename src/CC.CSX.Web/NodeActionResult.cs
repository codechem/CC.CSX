namespace CC.CSX.Web;

using System.Text;
using Microsoft.AspNetCore.Mvc;
using CC.CSX;

public class NodeActionResult(HtmlNode node) : IActionResult
{
    public HtmlNode Node { get; } = node;

    public Task ExecuteResultAsync(ActionContext context)
    {
        var res = context.HttpContext.Response;
        var stream = res.BodyWriter.AsStream();
        res.ContentType = "text/html";
        var writer = new StreamWriter(stream, Encoding.UTF8) as TextWriter;
        Node.WriteTo(ref writer);
        return writer.FlushAsync();
    }
}
