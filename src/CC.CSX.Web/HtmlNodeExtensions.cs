using Microsoft.AspNetCore.Mvc;

namespace CC.CSX.Web;


public static class HtmlNodeExtensions
{
    public static IActionResult ToActionResult(this HtmlNode node) 
        => new NodeActionResult(node);

    public static HtmlResult Render(HtmlNode node) 
        => node.ToResponse();

    public static HtmlResult Render<T>(T model, Func<T, HtmlNode> nodeFunc)
        => nodeFunc.Invoke(model).ToResponse();
        
    public static HtmlResult ToResponse(this HtmlNode node) => new HtmlResult(node);
}