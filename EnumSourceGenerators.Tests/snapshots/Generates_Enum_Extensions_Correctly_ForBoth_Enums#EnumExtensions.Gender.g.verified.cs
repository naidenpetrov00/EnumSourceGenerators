//HintName: EnumExtensions.Gender.g.cs

namespace EnumSourceGenerators
{
    public static partial class GenderEnumExtensions
        {
        public static string ToStringFast(this Gender value)
            => value switch
            {
            Gender.Male => nameof(Gender.Male),
            Gender.Female => nameof(Gender.Female),
                    _ => value.ToString(),
            };
    
    }
}