namespace ArchPZ.Demo.Application.UserManagement.Commands;

using MediatR;
using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Application.Mapping;
using ArchPZ.Demo.Domain.UserManagement.Entities;
using ArchPZ.Demo.Domain.UserManagement.Repositories;
using ArchPZ.Demo.Domain.UserManagement.ValueObjects;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser != null)
            throw new InvalidOperationException($"РљРѕСЂРёСЃС‚СѓРІР°С‡ Р· email '{request.Email}' РІР¶Рµ С–СЃРЅСѓС”.");

        var name = new FullName(request.FirstName, request.LastName);
        var email = new Email(request.Email);

        var user = User.Create(name, email);

        await _userRepository.AddAsync(user, cancellationToken);

        return user.ToDto();
    }
}
