namespace ArchPZ.Demo.Domain.OrderManagement.ValueObjects;

using ArchPZ.Demo.Domain.Common;

public class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("РЎСѓРјР° РЅРµ РјРѕР¶Рµ Р±СѓС‚Рё РІС–Рґ'С”РјРЅРѕСЋ.", nameof(amount));
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Р’Р°Р»СЋС‚Р° РЅРµ РјРѕР¶Рµ Р±СѓС‚Рё РїРѕСЂРѕР¶РЅСЊРѕСЋ.", nameof(currency));

        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("РќРµ РјРѕР¶РЅР° РґРѕРґР°С‚Рё СЃСѓРјРё РІ СЂС–Р·РЅРёС… РІР°Р»СЋС‚Р°С….");
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Multiply(int quantity)
    {
        return new Money(Amount * quantity, Currency);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString() => $"{Amount:F2} {Currency}";
}
