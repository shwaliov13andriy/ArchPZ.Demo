namespace ArchPZ.Demo.Domain.UserManagement.ValueObjects;

using ArchPZ.Demo.Domain.Common;

public class FullName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }

    public FullName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Р†Рј'СЏ РЅРµ РјРѕР¶Рµ Р±СѓС‚Рё РїРѕСЂРѕР¶РЅС–Рј.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("РџСЂС–Р·РІРёС‰Рµ РЅРµ РјРѕР¶Рµ Р±СѓС‚Рё РїРѕСЂРѕР¶РЅС–Рј.", nameof(lastName));

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
