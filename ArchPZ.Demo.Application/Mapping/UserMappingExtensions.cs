namespace ArchPZ.Demo.Application.Mapping;

using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Domain.UserManagement.Entities;

public static class UserMappingExtensions
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Email = user.Email.Value,
            Role = user.Role.ToString(),
            IsActive = user.IsActive
        };
    }
}
