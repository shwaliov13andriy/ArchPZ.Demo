namespace ArchPZ.Demo.Domain.OrderManagement.Entities;

using ArchPZ.Demo.Domain.Common;
using ArchPZ.Demo.Domain.OrderManagement.ValueObjects;

public class OrderItem : Entity
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }

    private OrderItem() : base()
    {
        ProductName = string.Empty;
        UnitPrice = new Money(0, "UAH");
    }

    public OrderItem(Guid productId, string productName, int quantity, Money unitPrice)
        : base()
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("РќР°Р·РІР° РїСЂРѕРґСѓРєС‚Сѓ РЅРµ РјРѕР¶Рµ Р±СѓС‚Рё РїРѕСЂРѕР¶РЅСЊРѕСЋ.", nameof(productName));
        if (quantity <= 0)
            throw new ArgumentException("РљС–Р»СЊРєС–СЃС‚СЊ РїРѕРІРёРЅРЅР° Р±СѓС‚Рё Р±С–Р»СЊС€Рµ РЅСѓР»СЏ.", nameof(quantity));

        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice));
    }

    public Money GetTotalPrice() => UnitPrice.Multiply(Quantity);

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("РљС–Р»СЊРєС–СЃС‚СЊ РїРѕРІРёРЅРЅР° Р±СѓС‚Рё Р±С–Р»СЊС€Рµ РЅСѓР»СЏ.", nameof(newQuantity));

        Quantity = newQuantity;
        UpdatedAt = DateTime.UtcNow;
    }
}
