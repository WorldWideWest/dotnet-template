using System.Text.Json.Serialization;
using MediatR;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.ChangePassword;

public record ChangePasswordCommand(string OldPassword, string NewPassword)
    : IRequest<Result<object>>
{
    [JsonIgnore]
    public string Email { get; set; }

    public ChangePasswordDto ToDto() => new(OldPassword, NewPassword, Email);
};

public record ChangePasswordDto(string OldPassword, string NewPassword, string Email);
