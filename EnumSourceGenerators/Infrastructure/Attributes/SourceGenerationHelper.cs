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
}
