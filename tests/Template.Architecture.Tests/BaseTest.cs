using System.Reflection;
using Template.Api.Controllers;
using Template.Application.Identity.Commands.CreateUser;
using Template.Domain.Identity.Entites;
using Template.Infrastructure.Identity.Services;

namespace Template.Architecture.Tests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(User).Assembly;
    protected static readonly Assembly ApplicationAssembly =
        typeof(CreateUserCommandHandler).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(IdentityService).Assembly;
    protected static readonly Assembly ApiAssembly = typeof(IdentityController).Assembly;

    public static TheoryData<string> DomainAssemblyTestData =>
        new TheoryData<string>
        {
            { ApplicationAssembly.GetName().Name },
            { InfrastructureAssembly.GetName().Name },
            { ApiAssembly.GetName().Name },
        };

    public static TheoryData<string> ApplicationAssemblyTestData =>
        new TheoryData<string>
        {
            { InfrastructureAssembly.GetName().Name },
            { ApiAssembly.GetName().Name },
        };

    public static TheoryData<string> InfrastructureAssemblyTestData =>
        new TheoryData<string> { { ApiAssembly.GetName().Name }, };
}
