namespace ArchPZ.Demo.Domain.UserManagement.ValueObjects;

using ArchPZ.Demo.Domain.Common;

/// <summary>
/// Value Object — Повне ім'я користувача.
/// </summary>
public class FullName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }

    public FullName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Ім'я не може бути порожнім.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Прізвище не може бути порожнім.", nameof(lastName));

        FirstName = firstName;
        LastName = lastName;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }

    public override string ToString() => $"{FirstName} {LastName}";
}
