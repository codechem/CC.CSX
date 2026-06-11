using CC.CSX;

using static CC.CSX.HtmlAttributes;
using static CC.CSX.HtmlElements;

public static class Templates
{
    public static HtmlNode MainPage(HtmlItem? extraHeaders = null, params HtmlItem[] children) => Html(
      Head(Title("Hello, World!"), UIKitImports, NunjucksImports, HtmxImports, HtmxJsonEncode, HtmxNunjucksTemplates,
          extraHeaders ?? None),
      Body(
        Div([@class("uk-container"), ("hx-ext", "client-side-templates"), .. children])));

    public static HtmlNode NunjucksImports = ScriptSrc("https://unpkg.com/nunjucks@3.2.3/browser/nunjucks.js");

    public static Fragment HtmxImports = Fragment(
        ScriptSrc("https://unpkg.com/htmx.org@1.9.3"),
        ScriptSrc("https://unpkg.com/hyperscript.org@0.9.7"));

    public static Fragment UIKitImports = Fragment(
        Link(href("https://cdn.jsdelivr.net/npm/uikit@3.16.23/dist/css/uikit.min.css"),
            rel("stylesheet")),
        ScriptSrc("https://cdn.jsdelivr.net/npm/uikit@3.16.23/dist/js/uikit.min.js"),
        ScriptSrc("https://cdn.jsdelivr.net/npm/uikit@3.16.23/dist/js/uikit-icons.min.js"));

    public const string NunjucksTemplateScript =
        @"htmx.defineExtension('client-side-templates', {
            transformResponse: function(text, xhr, elt) {
                var el = htmx.closest(elt, '[template]');
                if (el) {
                    var data = { data: JSON.parse(text) };
                    var tmplId = el.getAttribute('template');
                    if (tmplId.indexOf('#') == 0) {
                        var template = htmx.find(tmplId);
                        if (template)
                            return nunjucks.renderString(template.innerHTML, data);
                        throw 'Unknown template: ' + tmplId;
                    } else {
                        return nunjucks.render(tmplId, data);
                    }
                }
                return text;
            }
        });";

    public const string JsonEncodeScript =
        @"htmx.defineExtension('json-enc', {
            onEvent: function (name, evt) {
                if (name === 'htmx:configRequest') {
                    evt.detail.headers['Content-Type'] = 'application/json';
                }
            },
            encodeParameters : function(xhr, parameters, elt) {
                xhr.overrideMimeType('text/json');
                var items = new FormData(elt).entries();
                JSON.stringify(items);
                return (JSON.stringify(parameters));
            }
        });";

    public static HtmlNode HtmxNunjucksTemplates = Script(NunjucksTemplateScript);

    public static HtmlNode HtmxJsonEncode = Script(JsonEncodeScript);
}
