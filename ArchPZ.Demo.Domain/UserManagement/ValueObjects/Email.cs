namespace ArchPZ.Demo.Domain.UserManagement.ValueObjects;

using System.Text.RegularExpressions;
using ArchPZ.Demo.Domain.Common;

public class Email : ValueObject
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email РЅРµ РјРѕР¶Рµ Р±СѓС‚Рё РїРѕСЂРѕР¶РЅС–Рј.", nameof(value));

        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("РќРµРІС–СЂРЅРёР№ С„РѕСЂРјР°С‚ email.", nameof(value));

        Value = value.ToLowerInvariant();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
