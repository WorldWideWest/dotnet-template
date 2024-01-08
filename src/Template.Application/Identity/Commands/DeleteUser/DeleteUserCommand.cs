using System.Text.Json.Serialization;
using MediatR;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.DeleteUser;

public record DeleteUserCommand : IRequest<Result<object>>
{
    [JsonIgnore]
    public string Email { get; set; }
};
