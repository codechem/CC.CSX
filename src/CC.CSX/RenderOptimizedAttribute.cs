namespace CC.CSX;

/// <summary>
/// Marks a method that returns an <see cref="HtmlNode"/> for render-plan analysis: the
/// CC.CSX.RenderPlan.Generator inspects its body and decomposes it into static (bakeable) chunks
/// and dynamic holes. (Spike stage: the generator only reports the decomposition; codegen +
/// interceptors come later.)
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public sealed class RenderOptimizedAttribute : Attribute;
