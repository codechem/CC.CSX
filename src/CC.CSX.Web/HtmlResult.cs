namespace CC.CSX.Web;

using System.Text;
using System.Reflection;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Metadata;
using CC.CSX;

#if NET7_0_OR_GREATER
public class HtmlResult : IResult, IEndpointMetadataProvider
#else
public class HtmlResult : IResult
#endif
{
     public HtmlNode Node { get; private set;}

     public HtmlResult(HtmlNode node)
     {
         Node = node;
     }


    public Task ExecuteAsync(HttpContext context)
    {
        var res = context.Response;
        var stream = res.BodyWriter.AsStream();
        res.ContentType = "text/html";
        var writer = new StreamWriter(stream, Encoding.UTF8) as TextWriter;
        Node.WriteTo(ref writer);
        return writer.FlushAsync();
    }

    public static void PopulateMetadata(MethodInfo method, EndpointBuilder builder)
    {
        builder.Metadata.Add(new ProducesAttribute(MediaTypeNames.Text.Html));
    }
}