namespace CC.CSX.Web;

using System.Text;
using System.Reflection;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using CC.CSX;
using Microsoft.AspNetCore.Http.Metadata;

/// <summary>
/// Represents an <see cref="IResult"/> that when executed will write HTML to the response.
/// </summary>
#if NET7_0_OR_GREATER
public class HtmlResult : IResult, IEndpointMetadataProvider
#else
public class HtmlResult : IResult
#endif
{
    /// <summary>
    /// Gets the <see cref="HtmlNode"/> to render.
    /// </summary>
     public HtmlNode Node { get; private set;}

     /// <summary>
     /// Initializes a new instance of <see cref="HtmlResult"/>.
     /// </summary>
     public HtmlResult(HtmlNode node)
     {
         Node = node;
     }


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
}