using NetEscapades.EnumGenerators;

namespace EnumSourceGenerators;

public readonly record struct EnumToGenerate
{
    public readonly string Name;
    public readonly EquatableArray<string> Values;

    public EnumToGenerate(string name, List<string> values)
    {
        this.Name = name;
        this.Values = new(values.ToArray());
    }
}
