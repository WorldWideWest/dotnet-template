using MediatR;
using Microsoft.AspNetCore.Http;
using Template.Application.IdentityServer.Common;
using Template.Domain.Common.Models;

namespace Template.Application.IdentityServer.Queries.GetProvider;

public record GetProviderQuery(string ReturnUrl, HttpRequest Request)
    : IRequest<Result<AuthenticationPropertiesResponse>>;
