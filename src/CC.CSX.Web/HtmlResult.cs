namespace CC.CSX.Web;

using System.Net.Mime;
using System.Reflection;
using System.Text;

using CC.CSX;

using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Represents an <see cref="IResult"/> that when executed will write HTML to the response.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="HtmlResult"/>.
/// </remarks>
#if NET7_0_OR_GREATER
public class HtmlResult(HtmlNode node) : IResult, IActionResult, IEndpointMetadataProvider
#else
public class HtmlResult : IResult, IActionResult
#endif
{
    /// <summary>
    /// Gets the <see cref="HtmlNode"/> to render.
    /// </summary>
    public HtmlNode Node { get; private set; } = node;


    /// <inheritdoc />
    public Task ExecuteAsync(HttpContext context)
    {
        var res = context.Response;
        var stream = res.BodyWriter.AsStream();
        res.ContentType = "text/html";
        var writer = new StreamWriter(stream, Encoding.UTF8) as TextWriter;
        Node.WriteTo(ref writer);
        return writer.FlushAsync();
    }

    /// <inheritdoc />
    public static void PopulateMetadata(MethodInfo method, EndpointBuilder builder)
    {
        builder.Metadata.Add(new ProducesAttribute(MediaTypeNames.Text.Html));
    }

    /// <inheritdoc />
    public Task ExecuteResultAsync(ActionContext context)
    {
        return ExecuteAsync(context.HttpContext);
    }
}