//HintName: EnumExtensions.Color.g.cs

        namespace EnumSourceGenerators
        {
            public static class EnumExtensions
            {
                public static string ToStringFast(thisColor value)
                => value switch
                    {
            Color.Red => nameof(Color.Red),
            Color.Blue => nameof(Color.Blue),
                    _ => value.ToString(),
                };
    
        }
    }