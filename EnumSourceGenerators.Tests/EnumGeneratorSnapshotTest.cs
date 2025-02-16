using System.Runtime.CompilerServices;
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

    private void SetGeneratedFileNameToTheMethodName([CallerMemberName] string testName = "")
    {
        this.verifySettings.UseFileName(testName);
    }

    [Fact]
    public Task Generates_Enum_Extensions_Correctly()
    {
        SetGeneratedFileNameToTheMethodName();

        // _output.WriteLine("Test is running");
        var source =
            @"
using EnumSourceGenerators;

[EnumExtensions]
public enum Color
{
    Red = 0,
    Blue = 1,
}";

        // _output.WriteLine(source);
        return TestHelper.VerifySource(source, this.output, this.verifySettings);
    }

    [Fact]
    public Task Generates_Enum_Extensions_Correctly_ForBoth_Enums()
    {
        SetGeneratedFileNameToTheMethodName();

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
[EnumExtensions]
public enum Gender
{
    Male = 0,
    Female = 1,
}
";

        return TestHelper.VerifySource(source, this.output, this.verifySettings);
    }

    [Fact]
    public Task Generates_Only_For_TheOne_WithThe_Attribute()
    {
        SetGeneratedFileNameToTheMethodName();

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

public enum Gender
{
    Male = 0,
    Female = 1,
}
";

        return TestHelper.VerifySource(source, this.output, this.verifySettings);
    }

    [Fact]
    public Task Not_Generating_Without_The_Namespace()
    {
        SetGeneratedFileNameToTheMethodName();

        // _output.WriteLine("Test is running");
        var source =
            @"
using EnumSourceGenerators;

[EnumExtensions]
public enum Color
{
    Red = 0,
    Blue = 1,
}";

        // _output.WriteLine(source);
        return TestHelper.VerifySource(source, this.output, this.verifySettings);
    }

    [Fact]
    public Task Not_Generating_For_Enum_With_No_Attribute()
    {
        SetGeneratedFileNameToTheMethodName();

        var source = @"
using EnumSourceGenerators;

    public enum Color
{
    Red = 0,
    Blue = 1,
}";

        return TestHelper.VerifySource(source, this.output, this.verifySettings);
    }

    [Fact]
    public Task Not_Generating_For_Enum_With_Other_Attribute()
    {
        SetGeneratedFileNameToTheMethodName();

        var source = @"
using EnumSourceGenerators;

[Flags]
public enum Color
{
    Red = 0,
    Blue = 1,
}";

        return TestHelper.VerifySource(source, this.output, this.verifySettings);
    }
}
