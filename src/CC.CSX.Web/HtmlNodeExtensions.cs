using Microsoft.AspNetCore.Mvc;

namespace CC.CSX.Web;

/// <summary>
/// Extension methods for <see cref="HtmlNode"/> to allow rendering to <see cref="IActionResult"/> and <see cref="HtmlResult"/>.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Converts the <see cref="HtmlNode"/> to an <see cref="IActionResult"/>.
    /// </summary>
    public static IActionResult ToActionResult(this HtmlNode node) 
        => new NodeActionResult(node);

    /// <summary>
    /// Converts the <see cref="HtmlNode"/> to an <see cref="HtmlResult"/>.
    /// </summary>
    public static HtmlResult Render(HtmlNode node) 
        => node.ToResponse();

    /// <summary>
    /// Converts the <see cref="HtmlNode"/> to an <see cref="HtmlResult"/>, while passing the model to the node function.
    /// </summary>
    public static HtmlResult Render<T>(T model, Func<T, HtmlNode> nodeFunc)
        => nodeFunc.Invoke(model).ToResponse();
        
    /// <summary>
    /// Converts the <see cref="HtmlNode"/> to an <see cref="HtmlResult"/>.
    /// </summary>
    public static HtmlResult ToResponse(this HtmlNode node) => new HtmlResult(node);
}