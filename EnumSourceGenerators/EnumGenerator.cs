using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

[Generator]
class EnumGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
            ctx.AddSource(
                "EnumExtensionsAttribute.g.cs",
                SourceText.From(SourceGenerationHelper.Attribute)
            )
        );
    }
}
