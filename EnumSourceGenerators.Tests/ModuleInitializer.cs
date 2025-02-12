using System.Runtime.CompilerServices;
using VerifyTests;

namespace EnumSourceGenerators.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init() => VerifySourceGenerators.Initialize();
}
