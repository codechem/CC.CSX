using CC.CSX.Css;

using static CC.CSX.HtmlElements;
using static CC.CSX.Css.CssAttributes;
using static CC.CSX.Css.CssProperties;

Print(
    Ul([ style(fontFamily("monospace"), fontSize(14.px())),
        .. from n in Enumerable.Range(1, 20)
        select Li(
            "0 =",
            (from m in Enumerable.Range(1, 20)
            where m % n == 0
            select B(" ≡ ", m)).ToList(),
            I(B(" mod "+n))
        )
    ])
);

static void Print(object node) { Console.WriteLine(node.ToString()); }
