//HintName: EnumExtensions.Color.g.cs

namespace EnumSourceGenerators
{
    public static partial class ColorEnumExtensions
        {
        public static string ToStringFast(this Color value)
            => value switch
            {
            Color.Red => nameof(Color.Red),
            Color.Blue => nameof(Color.Blue),
                    _ => value.ToString(),
            };
    
    }
}