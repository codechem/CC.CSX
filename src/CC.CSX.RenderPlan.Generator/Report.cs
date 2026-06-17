using System.Collections.Generic;
using System.Text;

namespace CC.CSX.RenderPlan.Generator;

/// <summary>Renders a readable decomposition report for a planned method.</summary>
internal static class Report
{
    public static string Render(MethodPlan plan)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"  ═══ {plan.Signature} ═══");
        if (plan.Plan is null)
        {
            sb.AppendLine("    (no single return expression — not analyzable)");
            return sb.ToString();
        }

        int statics = 0, holes = 0, loops = 0, opaque = 0, bytes = 0;
        sb.AppendLine("    plan:");
        Walk(plan.Plan, sb, "      ", ref statics, ref holes, ref loops, ref opaque, ref bytes);

        sb.AppendLine($"    template: {Template(plan.Plan)}");
        sb.AppendLine($"    stats   : {statics} static ({bytes} B), {holes} hole(s), {loops} loop(s), {opaque} opaque");
        sb.AppendLine(opaque == 0
            ? "    verdict : fully decomposable"
            : $"    verdict : {opaque} opaque boundary(ies) (rendered live)");
        return sb.ToString();
    }

    static void Walk(List<Segment> segs, StringBuilder sb, string indent,
        ref int s, ref int h, ref int l, ref int o, ref int bytes)
    {
        foreach (var seg in segs)
            switch (seg)
            {
                case StaticSeg st:
                    s++; bytes += Utf8Len(st.Text);
                    sb.AppendLine($"{indent}STATIC  {Utf8Len(st.Text),4}B  {Quote(st.Text)}");
                    break;
                case HoleSeg hole:
                    h++;
                    sb.AppendLine($"{indent}HOLE    {hole.Kind,-5} {hole.Expr}");
                    break;
                case OpaqueSeg op:
                    o++;
                    sb.AppendLine($"{indent}OPAQUE  {op.Kind,-5} {op.Expr}");
                    break;
                case LoopSeg loop:
                    l++;
                    sb.AppendLine($"{indent}LOOP    each {loop.ItemVar} in {loop.Items}");
                    Walk(loop.Body, sb, indent + "  ", ref s, ref h, ref l, ref o, ref bytes);
                    break;
            }
    }

    static string Template(List<Segment> segs)
    {
        var sb = new StringBuilder();
        foreach (var seg in segs)
            sb.Append(seg switch
            {
                StaticSeg st => st.Text,
                HoleSeg hole => "{" + hole.Expr + "}",
                OpaqueSeg op => "{opaque:" + op.Expr + "}",
                LoopSeg loop => "{each " + loop.ItemVar + " in " + loop.Items + ": " + Template(loop.Body) + "}",
                _ => "",
            });
        return sb.ToString();
    }

    static int Utf8Len(string s) => Encoding.UTF8.GetByteCount(s);
    static string Quote(string s)
    {
        s = s.Replace("\n", "\\n").Replace("\r", "\\r");
        if (s.Length > 60) s = s.Substring(0, 57) + "...";
        return "\"" + s + "\"";
    }
}
