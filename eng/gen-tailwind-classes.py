#!/usr/bin/env python3
"""Generate src/CC.CSX.Css.Tailwind/Tw.Classes.g.cs — strongly typed Tailwind default-theme
utility classes. Self-contained (theme data embedded below). Run from the repo root."""
import re

OUT = "src/CC.CSX.Css.Tailwind/Tw.Classes.g.cs"

KEYWORDS = {
    "abstract","as","base","bool","break","byte","case","catch","char","checked","class","const",
    "continue","decimal","default","delegate","do","double","else","enum","event","explicit",
    "extern","false","finally","fixed","float","for","foreach","goto","if","implicit","in","int",
    "interface","internal","is","lock","long","namespace","new","null","object","operator","out",
    "override","params","private","protected","public","readonly","ref","return","sbyte","sealed",
    "short","sizeof","stackalloc","static","string","struct","switch","this","throw","true","try",
    "typeof","uint","ulong","unchecked","unsafe","ushort","using","virtual","void","volatile","while",
}

SPACING = ["0","px","0.5","1","1.5","2","2.5","3","3.5","4","5","6","7","8","9","10","11","12",
           "14","16","20","24","28","32","36","40","44","48","52","56","60","64","72","80","96"]
FRACTIONS = ["1/2","1/3","2/3","1/4","2/4","3/4","1/5","2/5","3/5","4/5","1/6","5/6"]
TWELFTHS = ["1/12","5/12","7/12","11/12"]
COLOR_FAMILIES = ["slate","gray","zinc","neutral","stone","red","orange","amber","yellow","lime",
                  "green","emerald","teal","cyan","sky","blue","indigo","violet","purple","fuchsia","pink","rose"]
SHADES = ["50","100","200","300","400","500","600","700","800","900","950"]
COLOR_SPECIALS = ["white","black","transparent","current","inherit"]
COLOR_UTILS = ["bg","text","border","ring"]

classes: list[str] = []

def add(*names: str) -> None:
    classes.extend(names)

def scale(prefix: str, values: list[str]) -> None:
    add(*(f"{prefix}-{v}" for v in values))

# layout
add("container","block","inline-block","inline","flex","inline-flex","table","inline-table",
    "table-caption","table-cell","table-column","table-column-group","table-footer-group",
    "table-header-group","table-row-group","table-row","flow-root","grid","inline-grid",
    "contents","list-item","hidden")
add("static","fixed","absolute","relative","sticky")
add("visible","invisible","collapse")
add("isolate","isolation-auto","sr-only","not-sr-only","group","peer")
scale("aspect", ["auto","square","video"])
scale("object", ["contain","cover","fill","none","scale-down"])
for axis in ["","-x","-y"]:
    scale(f"overflow{axis}", ["auto","hidden","clip","visible","scroll"])
scale("overscroll", ["auto","contain","none"])

# flex & grid
add("flex-row","flex-row-reverse","flex-col","flex-col-reverse",
    "flex-wrap","flex-wrap-reverse","flex-nowrap",
    "flex-1","flex-auto","flex-initial","flex-none","grow","grow-0","shrink","shrink-0")
scale("grid-cols", [*map(str, range(1, 13)), "none","subgrid"])
scale("col-span", [*map(str, range(1, 13)), "full"])
scale("grid-rows", [*map(str, range(1, 7)), "none","subgrid"])
scale("row-span", [*map(str, range(1, 7)), "full"])
scale("grid-flow", ["row","col","dense","row-dense","col-dense"])
scale("order", [*map(str, range(1, 13)), "first","last","none"])
scale("items", ["start","end","center","baseline","stretch"])
scale("justify", ["normal","start","end","center","between","around","evenly","stretch"])
scale("content", ["normal","center","start","end","between","around","evenly","stretch","baseline"])
scale("self", ["auto","start","end","center","stretch","baseline"])
scale("place-items", ["start","end","center","baseline","stretch"])
scale("place-content", ["center","start","end","between","around","evenly","stretch","baseline"])
scale("place-self", ["auto","start","end","center","stretch"])

# spacing
for p in ["p","px","py","ps","pe","pt","pr","pb","pl"]:
    scale(p, SPACING)
for m in ["m","mx","my","ms","me","mt","mr","mb","ml"]:
    scale(m, [*SPACING, "auto"])
for g in ["gap","gap-x","gap-y"]:
    scale(g, SPACING)
for s in ["space-x","space-y"]:
    scale(s, SPACING)

# sizing
scale("w", [*SPACING, *FRACTIONS, *TWELFTHS, "auto","full","screen","min","max","fit"])
scale("h", [*SPACING, *FRACTIONS, "auto","full","screen","min","max","fit"])
scale("size", [*SPACING, "auto","full","min","max","fit"])
scale("min-w", ["0","full","min","max","fit"])
scale("max-w", ["none","xs","sm","md","lg","xl","2xl","3xl","4xl","5xl","6xl","7xl","full",
                "min","max","fit","prose","screen-sm","screen-md","screen-lg","screen-xl","screen-2xl"])
scale("min-h", ["0","full","screen","min","max","fit"])
scale("max-h", [*SPACING, "none","full","screen","min","max","fit"])

# position offsets
for o in ["inset","inset-x","inset-y","top","right","bottom","left"]:
    scale(o, [*SPACING, "auto","full","1/2"])
scale("z", ["0","10","20","30","40","50","auto"])

# typography
scale("text", ["xs","sm","base","lg","xl","2xl","3xl","4xl","5xl","6xl","7xl","8xl","9xl"])
scale("text", ["left","center","right","justify","start","end"])
add("text-ellipsis","text-clip","text-wrap","text-nowrap","text-balance","text-pretty","truncate")
scale("font", ["sans","serif","mono","thin","extralight","light","normal","medium","semibold","bold","extrabold","black"])
add("italic","not-italic","antialiased","subpixel-antialiased")
scale("leading", ["none","tight","snug","normal","relaxed","loose","3","4","5","6","7","8","9","10"])
scale("tracking", ["tighter","tight","normal","wide","wider","widest"])
add("underline","overline","line-through","no-underline")
add("uppercase","lowercase","capitalize","normal-case")
scale("whitespace", ["normal","nowrap","pre","pre-line","pre-wrap","break-spaces"])
scale("break", ["normal","words","all","keep"])
scale("align", ["baseline","top","middle","bottom","text-top","text-bottom","sub","super"])
scale("list", ["none","disc","decimal","inside","outside"])
scale("line-clamp", [*map(str, range(1, 7)), "none"])

# colors
for util in COLOR_UTILS:
    scale(util, COLOR_SPECIALS)
    for family in COLOR_FAMILIES:
        scale(util, [f"{family}-{shade}" for shade in SHADES])
add("bg-none")

# borders & rings
add("border")
scale("border", ["0","2","4","8"])
for side in ["x","y","t","r","b","l"]:
    add(f"border-{side}")
    scale(f"border-{side}", ["0","2","4","8"])
scale("border", ["solid","dashed","dotted","double","hidden","none"])
add("rounded")
scale("rounded", ["none","sm","md","lg","xl","2xl","3xl","full"])
for corner in ["t","r","b","l","tl","tr","br","bl"]:
    add(f"rounded-{corner}")
    scale(f"rounded-{corner}", ["none","sm","md","lg","xl","2xl","3xl","full"])
add("ring","ring-inset")
scale("ring", ["0","1","2","4","8"])
scale("ring-offset", ["0","1","2","4","8"])
add("outline","outline-none","outline-dashed","outline-dotted","outline-double")

# effects & filters
add("shadow","shadow-inner","shadow-none")
scale("shadow", ["sm","md","lg","xl","2xl"])
scale("opacity", ["0","5","10","15","20","25","30","40","50","60","70","75","80","90","95","100"])
scale("mix-blend", ["normal","multiply","screen","overlay","darken","lighten"])
add("blur","blur-none")
scale("blur", ["sm","md","lg","xl","2xl","3xl"])

# transforms & transitions
scale("scale", ["0","50","75","90","95","100","105","110","125","150"])
scale("rotate", ["0","1","2","3","6","12","45","90","180"])
for t in ["translate-x","translate-y"]:
    scale(t, [*SPACING, "1/2","full"])
add("transition","transition-none","transition-all","transition-colors","transition-opacity",
    "transition-shadow","transition-transform")
scale("duration", ["75","100","150","200","300","500","700","1000"])
scale("delay", ["75","100","150","200","300","500","700","1000"])
scale("ease", ["linear","in","out","in-out"])
scale("animate", ["none","spin","ping","pulse","bounce"])

# interactivity
scale("cursor", ["auto","default","pointer","wait","text","move","help","not-allowed","none","grab","grabbing"])
scale("select", ["none","text","all","auto"])
add("pointer-events-none","pointer-events-auto")
add("resize","resize-none","resize-x","resize-y")
add("scroll-auto","scroll-smooth")
scale("appearance", ["none","auto"])
scale("will-change", ["auto","scroll","contents","transform"])

# (method name, tailwind utility prefix) pairs for arbitrary values: mt("17px") -> mt-[17px]
ARBITRARY = [
    ("p","p"),("px","px"),("py","py"),("pt","pt"),("pr","pr"),("pb","pb"),("pl","pl"),
    ("m","m"),("mx","mx"),("my","my"),("mt","mt"),("mr","mr"),("mb","mb"),("ml","ml"),
    ("gap","gap"),("gapX","gap-x"),("gapY","gap-y"),
    ("w","w"),("h","h"),("size","size"),("minW","min-w"),("maxW","max-w"),("minH","min-h"),("maxH","max-h"),
    ("text","text"),("bg","bg"),("font","font"),("leading","leading"),("tracking","tracking"),
    ("top","top"),("right","right"),("bottom","bottom"),("left","left"),("inset","inset"),
    ("z","z"),("translateX","translate-x"),("translateY","translate-y"),
    ("gridCols","grid-cols"),("gridRows","grid-rows"),
]


def to_identifier(name: str) -> str:
    name = name.replace(".", "_").replace("/", "of")  # w-1.5 -> w1_5, w-1/2 -> w1of2
    segments = name.split("-")
    out = segments[0] + "".join(s[:1].upper() + s[1:] for s in segments[1:] if s)
    if out[0].isdigit():
        out = "_" + out
    if out in KEYWORDS:
        out = "@" + out
    return out


def main() -> None:
    fields: dict[str, str] = {}
    for css in classes:
        identifier = to_identifier(css)
        if identifier in fields:
            if fields[identifier] != css:
                raise SystemExit(f"identifier collision: {identifier} for {css} and {fields[identifier]}")
            continue
        fields[identifier] = css

    out = []
    out.append("// <auto-generated>")
    out.append("// Generated by eng/gen-tailwind-classes.py (Tailwind default theme). Do not edit by hand.")
    out.append("// </auto-generated>")
    out.append("namespace CC.CSX.Css.Tailwind;")
    out.append("")
    out.append("using CC.CSX.Css;")
    out.append("")
    out.append("public static partial class Tw")
    out.append("{")
    for identifier, css in fields.items():
        out.append(f"    ///<summary><c>{css}</c></summary>")
        out.append(f"    public static readonly CssClass {identifier} = new(\"{css}\");")
    out.append("")
    skipped = []
    for method, prefix in ARBITRARY:
        if method in fields:
            skipped.append(method)
            continue
        out.append(f"    ///<summary>Arbitrary value: <c>{prefix}-[…]</c></summary>")
        out.append(f"    public static CssClass {method}(string value) => new($\"{prefix}-[{{value}}]\");")
    out.append("}")

    with open(OUT, "w") as f:
        f.write("\n".join(out) + "\n")
    print(f"generated {len(fields)} classes, {len(ARBITRARY) - len(skipped)} arbitrary-value methods"
          + (f" (skipped colliding: {', '.join(skipped)})" if skipped else ""))


if __name__ == "__main__":
    main()
