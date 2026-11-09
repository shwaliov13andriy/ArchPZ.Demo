namespace ArchPZ.Demo.Application.UserManagement.Commands;

using MediatR;
using ArchPZ.Demo.Application.DTOs;

public record CreateUserCommand : IRequest<UserDto>
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}
