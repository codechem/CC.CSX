global using CC.CSX;
global using CC.CSX.Css;

global using Microsoft.AspNetCore.Mvc;

global using static CC.CSX.HtmlAttributes;
global using static CC.CSX.HtmlElements;
global using static CC.CSX.Htmx.HtmxAttributes;
global using static CC.CSX.Web.Extensions;
global using static CC.CSX.Css.CssAttributes;
global using static CC.CSX.Css.CssProperties;
global using static Web.Css;

// for the few property names that collide with HtmlAttributes (border, color, content, height, translate, width)
global using CSS = CC.CSX.Css.CssProperties;