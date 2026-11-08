namespace ArchPZ.Demo.Domain.OrderManagement.ValueObjects;

using ArchPZ.Demo.Domain.Common;

/// <summary>
/// Value Object — Адреса доставки.
/// Незмінний об'єкт, що порівнюється за значенням, а не за посиланням.
/// </summary>
public class Address : ValueObject
{
    public string Street { get; }
    public string City { get; }
    public string ZipCode { get; }
    public string Country { get; }

    public Address(string street, string city, string zipCode, string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Вулиця не може бути порожньою.", nameof(street));
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Місто не може бути порожнім.", nameof(city));
        if (string.IsNullOrWhiteSpace(zipCode))
            throw new ArgumentException("Поштовий індекс не може бути порожнім.", nameof(zipCode));
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Країна не може бути порожньою.", nameof(country));

        Street = street;
        City = city;
        ZipCode = zipCode;
        Country = country;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return ZipCode;
        yield return Country;
    }

    public override string ToString() => $"{Street}, {City}, {ZipCode}, {Country}";
}
