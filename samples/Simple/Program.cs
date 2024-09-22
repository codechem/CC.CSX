using static CC.CSX.HtmlElements;

Print(
    Ul([ .. from n in Enumerable.Range(1, 20)
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