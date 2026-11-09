namespace ArchPZ.Demo.Domain.UserManagement.Entities;

using ArchPZ.Demo.Domain.Common;
using ArchPZ.Demo.Domain.UserManagement.Enums;
using ArchPZ.Demo.Domain.UserManagement.ValueObjects;

public class User : AggregateRoot
{
    public FullName Name { get; private set; }
    public Email Email { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }

    private User() : base()
    {
        Name = null!;
        Email = null!;
    }

    public static User Create(FullName name, Email email, UserRole role = UserRole.Customer)
    {
        return new User
        {
            Name = name ?? throw new ArgumentNullException(nameof(name)),
            Email = email ?? throw new ArgumentNullException(nameof(email)),
            Role = role,
            IsActive = true
        };
    }

    public void UpdateName(FullName newName)
    {
        Name = newName ?? throw new ArgumentNullException(nameof(newName));
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateEmail(Email newEmail)
    {
        Email = newEmail ?? throw new ArgumentNullException(nameof(newEmail));
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("РљРѕСЂРёСЃС‚СѓРІР°С‡Р° РІР¶Рµ РґРµР°РєС‚РёРІРѕРІР°РЅРѕ.");
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        if (IsActive)
            throw new InvalidOperationException("РљРѕСЂРёСЃС‚СѓРІР°С‡ РІР¶Рµ Р°РєС‚РёРІРЅРёР№.");
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
