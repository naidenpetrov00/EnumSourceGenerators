using System.Text;
using EnumSourceGenerators;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class SourceGenerationHelper
{
    public const string Attribute =
        @"
namespace EnumSourceGenerators
{
    [System.AttributeUsage(System.AttributeTargets.Enum)]
    public class EnumExtensionsAttribute : System.Attribute
    {
    }
}";

    public static string GenerateExtensionClass(EnumToGenerate enumToGenerate)
    {
        var sb = new StringBuilder();

        sb.Append(@"
namespace EnumSourceGenerators
{
    public static partial class ").Append(enumToGenerate.ExtensionName);
            sb.Append(@"
        {
        public static string ToStringFast(this ").Append(enumToGenerate.Name).Append(@" value)
            => value switch
            {");
        foreach (var member in enumToGenerate.Values)
        {
            sb.Append(@"
            ").Append(enumToGenerate.Name).Append('.').Append(member)
            .Append(" => nameof(")
            .Append(enumToGenerate.Name).Append('.').Append(member).Append("),");
        }

        sb.Append(@"
                    _ => value.ToString(),
            };
    ");
        sb.Append(@"
    }
}");

        return sb.ToString();
    }
}

public enum ColourTemplate
{
    Red,
    Blue
}
//The GenerateExtensionClass should generate this i use it as template
public static class EnumExtensionsTemplate
{
    public static string ToStringFast(this ColourTemplate value) 
    => value switch
    {
        ColourTemplate.Red => nameof(ColourTemplate.Red),
        ColourTemplate.Blue => nameof(ColourTemplate.Blue),
        _ => value.ToString(),
    };
}
