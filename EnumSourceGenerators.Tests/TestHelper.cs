using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit.Abstractions;

namespace EnumSourceGenerators.Tests;

public class TestHelper
{
    public static async Task<SettingsTask> Verify(string source, ITestOutputHelper _output)
    {
        //Paresing the string to a c# syntax tree
        var syntaxTree = CSharpSyntaxTree.ParseText(source);

        //Create compilation for the syntax tree
        var compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: [syntaxTree]
        );

        var generator = new EnumGenerator();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGenerators(compilation);

        return Verifier.Verify(driver);
    }
}
