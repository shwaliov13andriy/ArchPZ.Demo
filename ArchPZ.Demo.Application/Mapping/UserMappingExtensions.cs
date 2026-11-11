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
            FullName = $"{user.Name.FirstName} {user.Name.LastName}",
            Email = user.Email.Value,
            Role = user.Role.ToString(),
            IsActive = user.IsActive
        };
    }

    public static IReadOnlyList<UserDto> ToDtoList(this IEnumerable<User> users)
    {
        return users.Select(ToDto).ToList();
    }
}
