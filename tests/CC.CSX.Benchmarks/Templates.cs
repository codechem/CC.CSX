using CC.CSX;
using static CC.CSX.HtmlAttributes;
using static CC.CSX.HtmlElements;
using System.Linq;

public static class Templates
{
    public static HtmlNode MaterialPage(HtmlItem? extraHeaders = null, params HtmlItem[] children) => Html(
      Head(
        Meta(charset("utf-8")),
        Meta(("http-equiv", "X-UA-Compatible"), content("IE=edge")),
        Title("Scheduler"),
        TailwindImports,
        HtmxImports,
        NunjucksImports,
        HtmxNunjucksTemplates,
        HtmxJsonEnconde,
        extraHeaders ?? None),
      Body(
        Div([@class("uk-container"), ("hx-ext", "client-side-templates"), ..children,
            Footer(
              @class("bg-neutral-200 text-center dark:bg-neutral-700 lg:text-left"),
              Div(@class("p-4 text-center text-neutral-700 dark:text-neutral-200"),
                "© 2023 :",
                A(@class("text-neutral-800 dark:text-neutral-400"),
                  href("https://www.codechem.com/"),
                  "from CC with 💚"))),
            // Tailwind Elements JavaScript 
            ScriptSrc("https://cdn.jsdelivr.net/npm/tw-elements/dist/js/tw-elements.umd.min.js")
        ])));

    public static HtmlNode MainPage(HtmlItem? extraHeaders = null, params HtmlItem[] children) => Html(
      Head(Title("Hello, World!"), UIKitImports, NunjucksImports, HtmxImports, HtmxJsonEnconde,HtmxNunjucksTemplates, 
          extraHeaders ?? None),
      Body(
        Div([@class("uk-container"), ("hx-ext","client-side-templates"), ..children])));

    public static HtmlNode TailwindImports = Fragment(
        Link(href("https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"), rel("stylesheet")),
        Link(href("https://cdn.jsdelivr.net/npm/tw-elements/dist/css/tw-elements.min.css"), rel("stylesheet")),
        ScriptSrc("https://cdn.tailwindcss.com/3.3.0"),
        Script(@"
              tailwind.config = {
                darkMode: 'class',
                theme: {
                  fontFamily: {
                    sans: ['Roboto', 'sans-serif'],
                    body: ['Roboto', 'sans-serif'],
                    mono: ['ui-monospace', 'monospace'],
                  },
                },
                corePlugins: {
                  preflight: false,
                },
              };
            "));
    public static HtmlNode NunjucksImports = ScriptSrc("https://unpkg.com/nunjucks@3.2.3/browser/nunjucks.js");
    public static Fragment HtmxImports = Fragment(
        ScriptSrc("https://unpkg.com/htmx.org@1.9.3"),
        ScriptSrc("https://unpkg.com/hyperscript.org@0.9.7"));

    public static Fragment UIKitImports = Fragment(
        Link(href("https://cdn.jsdelivr.net/npm/uikit@3.16.23/dist/css/uikit.min.css"),
            rel("stylesheet")),
        Link(href("https://cdn.jsdelivr.net/npm/uikit@3.16.23/dist/css/uikit.min.css"),
            rel("stylesheet")),
        ScriptSrc("https://cdn.jsdelivr.net/npm/uikit@3.16.23/dist/js/uikit.min.js"),
        ScriptSrc("https://cdn.jsdelivr.net/npm/uikit@3.16.23/dist/js/uikit-icons.min.js"));

    public static HtmlNode HtmxNunjucksTemplates = Script(
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
        });");

    public static HtmlNode HtmxJsonEnconde = Script(
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
        });");
    public static HtmlNode Menu(params HtmlNode[] items) =>
        Ul(("uk-tab", null), Fragment(
            items.Select((i, idx) =>
                Li(i).Add(idx == 0 ? ("uk-active", null) : ("", null))).ToArray()));

}