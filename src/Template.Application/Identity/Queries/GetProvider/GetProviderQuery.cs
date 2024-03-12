using MediatR;
using Microsoft.AspNetCore.Http;
using Template.Application.Identity.Common;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Queries.GetProvider;

public record GetProviderQuery(string ReturnUrl, HttpRequest Request)
    : IRequest<Result<AuthenticationPropertiesResponse>>;
