using CC.CSX;
using System.Text;
using static CC.CSX.HtmlAttributes;
using static CC.CSX.HtmlElements;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmarks>();

[MemoryDiagnoser(false)]
public class Benchmarks
{
    HtmlNode node= Templates.MainPage(null,
        Div(
            Form(@class("uk-form"), method("post"), action("/test"),
                Input(type("text"), name("name"), value("test")),
                Input(type("text"), name("age"), value("10")),
                Button(type("submit"), "Submit")
            )
        ));


    [Benchmark]
    public void Benchmark_ToString()
    {
        var a = node.ToString();
    }

    [Benchmark]
    public void Benchmark_StringBuilder()
    {
        var a = new StringBuilder();
        node.AppendTo(ref a);
    }


    [Benchmark]
    public void Benchmark_TextWriter()
    {
        var a = new StringWriter() as TextWriter;
        node.WriteTo(ref a);
    }

}