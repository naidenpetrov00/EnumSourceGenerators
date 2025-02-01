using System.Text;
using EnumSourceGenerators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

[Generator]
class EnumGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        System.Diagnostics.Debugger.Launch();

        //adds source code of the attribute to the compilation
        context.RegisterPostInitializationOutput(ctx =>
            ctx.AddSource(
                "EnumExtensionsAttribute.g.cs",
                SourceText.From(SourceGenerationHelper.Attribute, Encoding.UTF8)
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

        context.RegisterSourceOutput(enumsToGenerate, static (spc, source) => Execute(source, spc));
    }

    static void Execute(
        EnumToGenerate? enumToGenerate,
        SourceProductionContext sourceProductionContext
    )
    {
        if (enumToGenerate is { } value)
        {
            //generate the source code of the enum and add it to the output node
            string result = SourceGenerationHelper.GenerateExtensionClass(value);
            //create separate file for each enum
            sourceProductionContext.AddSource($"EnumExtensions.${value.Name}", result);
        }
    }

    //check idf node contains Attributes
    private static bool IsSyntaxTargetForGeneration(SyntaxNode node)
    {
        System.Diagnostics.Debugger.Launch();

        if (node is EnumDeclarationSyntax m && m.AttributeLists.Count > 0)
        {
            return true;
        }
        return false;
    }

    static EnumToGenerate? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        //context node is EnumDeclarationSyntax thank to predicate check
        var enumDeclarationSyntax = (EnumDeclarationSyntax)context.Node;

        foreach (var attributeSyntaxListSyntax in enumDeclarationSyntax.AttributeLists)
        {
            foreach (var attributeSyntax in attributeSyntaxListSyntax.Attributes)
            {
                //check if the attribute have a constructor (clear out some error cases i gues)
                if (
                    context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol
                    is not IMethodSymbol attributeSymbol
                )
                    continue;

                var attributeSymbolContaningType = attributeSymbol.ContainingType;
                var fullName = attributeSymbolContaningType.ToDisplayString();

                if (fullName == "EnumSourceGenerators.EnumExtensionsAttribute")
                {
                    return GetEnumToGenerate(context.SemanticModel, enumDeclarationSyntax);
                }
            }
        }

        return null;
    }

    private static EnumToGenerate? GetEnumToGenerate(
        SemanticModel semanticModel,
        EnumDeclarationSyntax enumDeclarationSyntax
    )
    {
        //check if its named type
        if (
            semanticModel.GetDeclaredSymbol(enumDeclarationSyntax)
            is not INamedTypeSymbol enumSymbol
        )
        {
            return null;
        }

        var enumName = enumSymbol.ToString();

        var enumMembers = enumSymbol.GetMembers();
        var members = new List<string>(enumMembers.Length);

        foreach (var member in enumMembers)
        {
            //get all the const fields
            //TODO: later check if this is needed by this time the values should be enums only
            // and enum field is always const
            if (member is IFieldSymbol field && field.ConstantValue is not null)
            {
                members.Add(member.Name);
            }
        }

        //TODO: Probable problem

        return new EnumToGenerate(enumName, members);
    }
}
