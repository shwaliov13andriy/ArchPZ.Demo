namespace ArchPZ.Demo.Application.Mapping;

using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Domain.UserManagement.Entities;

public class UserMapper : IUserMapper
{
    public UserDto ToDto(User user)
    {
        return user.ToDto();
    }
}
