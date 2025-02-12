using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EnumSourceGenerators.Tests;

public class EnumGeneratorSnapshotTest : VerifyBase
{
    private readonly ITestOutputHelper _output;

    public EnumGeneratorSnapshotTest(ITestOutputHelper output)
        : base()
    {
        _output = output;
    }

    [Fact]
    public Task GeneratesEnumExtensionsCorrectly()
    {
        // _output.WriteLine("Test is running");
        var source =
            @"
using EnumSourceGenerators;

[EnumExtensions]
public enum Color
{
    Red = 0,
    Blue = 1,
}
        ";

        // _output.WriteLine(source);
        return TestHelper.Verify(source, _output);
    }
}
