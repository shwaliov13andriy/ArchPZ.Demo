namespace ArchPZ.Demo.Application.Mapping;

using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Domain.UserManagement.Entities;

public class UserMapper : IUserMapper
{
    public UserDto ToDto(User user)
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
