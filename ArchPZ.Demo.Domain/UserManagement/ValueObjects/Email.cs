namespace ArchPZ.Demo.Domain.UserManagement.ValueObjects;

using System.Text.RegularExpressions;
using ArchPZ.Demo.Domain.Common;

/// <summary>
/// Value Object — Електронна адреса з валідацією формату.
/// </summary>
public class Email : ValueObject
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email не може бути порожнім.", nameof(value));

        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Невірний формат email.", nameof(value));

        Value = value.ToLowerInvariant();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
