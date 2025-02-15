using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EnumSourceGenerators.Tests;

public class EnumGeneratorSnapshotTest : VerifyBase
{
    private readonly ITestOutputHelper output;
    private readonly VerifySettings verifySettings;

    public EnumGeneratorSnapshotTest(ITestOutputHelper output)
        : base()
    {
        this.output = output;
        this.verifySettings = new VerifySettings();
        this.verifySettings.UseDirectory("snapshots");
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
        return TestHelper.VerifySource(source, this.output, this.verifySettings);
    }
}
