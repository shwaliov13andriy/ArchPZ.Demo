namespace ArchPZ.Demo.Domain.OrderManagement.ValueObjects;

using ArchPZ.Demo.Domain.Common;

public class Address : ValueObject
{
    public string Street { get; }
    public string City { get; }
    public string ZipCode { get; }
    public string Country { get; }

    public Address(string street, string city, string zipCode, string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Р’СѓР»РёС†СЏ РЅРµ РјРѕР¶Рµ Р±СѓС‚Рё РїРѕСЂРѕР¶РЅСЊРѕСЋ.", nameof(street));
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("РњС–СЃС‚Рѕ РЅРµ РјРѕР¶Рµ Р±СѓС‚Рё РїРѕСЂРѕР¶РЅС–Рј.", nameof(city));
        if (string.IsNullOrWhiteSpace(zipCode))
            throw new ArgumentException("РџРѕС€С‚РѕРІРёР№ С–РЅРґРµРєСЃ РЅРµ РјРѕР¶Рµ Р±СѓС‚Рё РїРѕСЂРѕР¶РЅС–Рј.", nameof(zipCode));
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("РљСЂР°С—РЅР° РЅРµ РјРѕР¶Рµ Р±СѓС‚Рё РїРѕСЂРѕР¶РЅСЊРѕСЋ.", nameof(country));

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
