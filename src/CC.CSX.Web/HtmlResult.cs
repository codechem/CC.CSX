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
        res.ContentType = "text/html";
        // PipeWriter is an IBufferWriter<byte>; render UTF-8 straight into it (no Stream/StreamWriter
        // wrapper, no large intermediate string) and flush once.
        Node.WriteTo(res.BodyWriter);
        return res.BodyWriter.FlushAsync().AsTask();
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