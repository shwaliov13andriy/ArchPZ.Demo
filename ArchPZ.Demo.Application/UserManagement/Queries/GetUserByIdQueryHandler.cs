namespace ArchPZ.Demo.Application.UserManagement.Queries;

using MediatR;
using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Application.Mapping;
using ArchPZ.Demo.Domain.UserManagement.Repositories;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IUserMapper userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        return user is null ? null : _userMapper.ToDto(user);
    }
}
