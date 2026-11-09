namespace ArchPZ.Demo.Application.UserManagement.Queries;

using MediatR;
using ArchPZ.Demo.Application.DTOs;

public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto?>;
