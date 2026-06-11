namespace CC.CSX.Css.Tests;

using System.Text;
using CC.CSX.Css.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

public class CssClassesGeneratorTests
{
    sealed class TestAdditionalText(string path, string text) : AdditionalText
    {
        public override string Path => path;
        public override SourceText GetText(CancellationToken ct = default) => SourceText.From(text, Encoding.UTF8);
    }

    static string RunGenerator(params TestAdditionalText[] files)
    {
        var driver = CSharpGeneratorDriver.Create(
            [new CssClassesGenerator().AsSourceGenerator()],
            additionalTexts: files);
        var compilation = CSharpCompilation.Create("Sample.Web",
            references: [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]);
        var result = driver.RunGenerators(compilation).GetRunResult();
        return string.Join("\n---\n", result.Results
            .SelectMany(r => r.GeneratedSources)
            .Select(s => s.SourceText.ToString()));
    }

    [Fact]
    public void Generator_Should_EmitTypedClassesPerFile()
    {
        var generated = RunGenerator(new TestAdditionalText("/styles/app.css",
            ".card { } .card--highlighted { } .btn-primary { }"));

        Assert.Contains("namespace Sample.Web", generated);
        Assert.Contains("public static partial class Css", generated);
        Assert.Contains("public static partial class App", generated);
        Assert.Contains("CssClass card = new global::CC.CSX.Css.CssClass(\"card\")", generated);
        Assert.Contains("CssClass cardHighlighted = new global::CC.CSX.Css.CssClass(\"card--highlighted\")", generated);
        Assert.Contains("CssClass btnPrimary = new global::CC.CSX.Css.CssClass(\"btn-primary\")", generated);
    }

    [Fact]
    public void Generator_Should_BakeSourceAndBundle()
    {
        var generated = RunGenerator(new TestAdditionalText("/styles/site.css", ".a { color: \"red\" }"));

        Assert.Contains("public const string Source = @\"", generated);
        Assert.Contains("\"\"red\"\"", generated); // verbatim-escaped quote
        Assert.Contains("CssBundle Bundle = new global::CC.CSX.Css.CssBundle(\"site\", Source)", generated);
    }

    [Fact]
    public void Generator_Should_EscapeKeywordIdentifiers()
    {
        var generated = RunGenerator(new TestAdditionalText("/k.css", ".static { } .fixed { }"));
        Assert.Contains("CssClass @static", generated);
        Assert.Contains("CssClass @fixed", generated);
    }

    [Fact]
    public void Generator_Should_EmitOneClassPerCssFile()
    {
        var generated = RunGenerator(
            new TestAdditionalText("/styles/app.css", ".a { }"),
            new TestAdditionalText("/styles/admin-area.css", ".b { }"));
        Assert.Contains("class App", generated);
        Assert.Contains("class AdminArea", generated);
    }

    [Fact]
    public void GeneratedCode_Should_MatchRuntimeBehavior()
    {
        // The same scanner the generator uses, applied to a stylesheet, must produce
        // identifiers that expand back to the original class names at runtime.
        var css = ".hero-section { } .hero-section__title { }";
        var names = CssScanner.ExtractClassNames(css);
        var composed = CssClass.Compose([.. names.Select(n => new CssClass(n))]);
        Assert.Equal("hero-section hero-section__title", composed.Name);
    }
}
