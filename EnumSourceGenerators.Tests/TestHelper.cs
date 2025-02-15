using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit.Abstractions;

namespace EnumSourceGenerators.Tests;

public class TestHelper
{
    public static Task VerifySource(string source, ITestOutputHelper output, VerifySettings verifySettings)
    {
        //Paresing the string to a c# syntax tree
        var syntaxTree = CSharpSyntaxTree.ParseText(source);

        // The compilation of the source doent have references and could fint System.Attribute
        IEnumerable<MetadataReference> references = [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            //Add the ecacts location if it doesnt find it form the object refference
            MetadataReference.CreateFromFile(typeof(System.Attribute).Assembly.Location),
        ];

        //Create compilation for the syntax tree
        var compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: [syntaxTree],
            references: references
        );

        var generator = new EnumGenerator();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGenerators(compilation);

        return Verify(driver.GetRunResult(), verifySettings);
    }
}
