using NetEscapades.EnumGenerators;

namespace EnumSourceGenerators;

public readonly record struct EnumToGenerate
{
    public readonly string ExtensionName;
    public readonly string Name;
    public readonly EquatableArray<string> Values;

    public EnumToGenerate(string extensionName,string name, List<string> values)
    {
        this.ExtensionName = extensionName;
        this.Name = name;
        this.Values = new(values.ToArray());
    }
}
