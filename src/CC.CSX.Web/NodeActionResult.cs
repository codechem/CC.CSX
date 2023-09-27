namespace CC.CSX.Web;

using System.Text;
using Microsoft.AspNetCore.Mvc;
using CC.CSX;

/// <summary>
/// Represents an <see cref="IResult"/> that when executed will write HTML to the response.
/// </summary>
public class NodeActionResult(HtmlNode node) : IActionResult
{
    /// <summary>
    /// The <see cref="HtmlNode"/> to render.
    /// </summary>
    public HtmlNode Node { get; } = node;

    /// <inheritdoc />
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