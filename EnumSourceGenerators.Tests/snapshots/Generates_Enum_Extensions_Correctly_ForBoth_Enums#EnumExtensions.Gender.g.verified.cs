//HintName: EnumExtensions.Gender.g.cs

        namespace EnumSourceGenerators
        {
            public static class EnumExtensions
            {
                public static string ToStringFast(thisGender value)
                => value switch
                    {
            Gender.Male => nameof(Gender.Male),
            Gender.Female => nameof(Gender.Female),
                    _ => value.ToString(),
                };
    
        }
    }