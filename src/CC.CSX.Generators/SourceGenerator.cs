using Microsoft.CodeAnalysis;
using System.Text;

namespace SourceGenerator
{
    [Generator]
    public class MyGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required
        }

        public void Execute(GeneratorExecutionContext context)
        {
            // Create a new source file that contains the generated code
            var sourceBuilder = new StringBuilder(@"using System;
namespace MyGeneratedCode
{
    public class MyClass
    {
        public void MyMethod()
        {
            Console.WriteLine(""Hello from generated code!"");
        }
    }
}");
            context.AddSource("MyGeneratedCode", sourceBuilder.ToString());
        }
    }
}