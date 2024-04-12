using FluentAssertions;

namespace Template.Architecture.Tests;

public class LayerTests : BaseTest
{
    [Theory]
    [MemberData(nameof(DomainAssemblyTestData))]
    public void Domain_Should_NotHaveDependencyAnyLayer(string assemblyName)
    {
        var result = Types
            .InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(assemblyName)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(ApplicationAssemblyTestData))]
    public void Application_Should_NotHaveDependencyOnApiAndInfrastructure(string assemblyName)
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(assemblyName)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(InfrastructureAssemblyTestData))]
    public void Infrastructure_Should_NotHaveDependencyApi(string assemblyName)
    {
        var result = Types
            .InAssembly(InfrastructureAssembly)
            .Should()
            .NotHaveDependencyOn(assemblyName)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
