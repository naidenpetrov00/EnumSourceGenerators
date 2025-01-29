using EnumSourceGenerators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

[Generator]
class EnumGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //adds source code of the attribute to the compilation
        context.RegisterPostInitializationOutput(ctx =>
            ctx.AddSource(
                "EnumExtensionsAttribute.g.cs",
                SourceText.From(SourceGenerationHelper.Attribute)
            )
        );

        IncrementalValuesProvider<EnumToGenerate?> enumsToGenerate = context
            .SyntaxProvider.CreateSyntaxProvider(
                // predicate runs on every keystroke
                //thats why we check if its only attribute but not if its [EnumExtensions] here!
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                //ctx: syntax notes that passed the predicate
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)
            )
            //filters out if transform have not found enum with attribute and is returning null
            .Where(static m => m is not null);
    }

    //check idf node contains Attributes
    private static bool IsSyntaxTargetForGeneration(SyntaxNode node)
    {
        if (node is EnumDeclarationSyntax m && m.AttributeLists.Count > 0)
        {
            return true;
        }
        return false;
    }

    static EnumToGenerate? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        //Check if enums contains [EnumExtensions]
        return null;
    }
}
