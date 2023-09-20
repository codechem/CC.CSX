namespace CC.CSX;

public delegate HtmlNode ComponentRenderDelegate<T>(T model);
public delegate HtmlNode ComponentRenderDelegate();

public static class HtmlComponents
{
    public static ComponentRenderDelegate<T> Component<T>(Func<T, HtmlNode> func) => (T model) => func(model);

    public static ComponentRenderDelegate Component(Func<HtmlNode> func) => () => func();
}