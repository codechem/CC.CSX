namespace CC.CSX.Web;

using System.Text;
using System.Reflection;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Metadata;
using CC.CSX;

public static class HtmlExtensions
{
    public static HtmlResult ToResponse(this HtmlNode node) => new HtmlResult(node);
}

#if NET7_0_OR_GREATER
public class HtmlResult : IResult, IEndpointMetadataProvider
#else
public class HtmlResult : IResult
#endif
{
    private readonly HtmlNode _node;
    public HtmlResult(HtmlNode node) => _node = node;

    public Task ExecuteAsync(HttpContext httpContext)
    {
        var sb = new StringBuilder();
        _node.AppendTo(ref sb);
        var content = sb.ToString();
        httpContext.Response.ContentType = MediaTypeNames.Text.Html;
        httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(content);
        return httpContext.Response.WriteAsync(content);
    }

    public static void PopulateMetadata(MethodInfo method, EndpointBuilder builder)
    {
        builder.Metadata.Add(new ProducesAttribute(MediaTypeNames.Text.Html));
    }
}