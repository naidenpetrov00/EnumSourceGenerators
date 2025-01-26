using NetEscapades.EnumGenerators;

namespace EnumSourceGenerators;

public readonly record struct EnumToGenerate
{
    private readonly string name;
    private readonly EquatableArray<string> values;

    public EnumToGenerate(string name, List<string> values)
    {
        this.name = name;
        this.values = new(values.ToArray());
    }
}
