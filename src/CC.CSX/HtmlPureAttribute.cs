namespace CC.CSX;

/// <summary>
/// Declares that a method is a pure builder of HTML: its result depends only on its arguments and
/// it has no side effects. The render-plan analyzer treats calls to such methods as static when all
/// their arguments are static (and may follow into them). The built-in CC.CSX element/attribute
/// factories are recognized automatically; apply this to your own pure component builders to opt
/// them into the same treatment.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public sealed class HtmlPureAttribute : Attribute;
