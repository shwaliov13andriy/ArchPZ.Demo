namespace ArchPZ.Demo.Domain.OrderManagement.Entities;

using ArchPZ.Demo.Domain.Common;
using ArchPZ.Demo.Domain.OrderManagement.Enums;
using ArchPZ.Demo.Domain.OrderManagement.Events;
using ArchPZ.Demo.Domain.OrderManagement.ValueObjects;

public class Order : AggregateRoot
{
    private readonly List<OrderItem> _items = new();

    public Guid CustomerId { get; private set; }
    public Address ShippingAddress { get; private set; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public string? Notes { get; private set; }

    private Order() : base()
    {
        ShippingAddress = null!;
    }

    public static Order Create(Guid customerId, Address shippingAddress, string? notes = null)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("Р†РґРµРЅС‚РёС„С–РєР°С‚РѕСЂ РєР»С–С”РЅС‚Р° РЅРµ РјРѕР¶Рµ Р±СѓС‚Рё РїРѕСЂРѕР¶РЅС–Рј.", nameof(customerId));

        var order = new Order
        {
            CustomerId = customerId,
            ShippingAddress = shippingAddress ?? throw new ArgumentNullException(nameof(shippingAddress)),
            Status = OrderStatus.Pending,
            Notes = notes
        };

        order.AddDomainEvent(new OrderCreatedEvent(order.Id, customerId));

        return order;
    }

    public void AddItem(Guid productId, string productName, int quantity, Money unitPrice)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("РњРѕР¶РЅР° РґРѕРґР°РІР°С‚Рё С‚РѕРІР°СЂРё Р»РёС€Рµ РґРѕ Р·Р°РјРѕРІР»РµРЅРЅСЏ Р·С– СЃС‚Р°С‚СѓСЃРѕРј 'РћС‡С–РєСѓС”'.");

        var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.UpdateQuantity(existingItem.Quantity + quantity);
        }
        else
        {
            _items.Add(new OrderItem(productId, productName, quantity, unitPrice));
        }

        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveItem(Guid itemId)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("РњРѕР¶РЅР° РІРёРґР°Р»СЏС‚Рё С‚РѕРІР°СЂРё Р»РёС€Рµ С–Р· Р·Р°РјРѕРІР»РµРЅРЅСЏ Р·С– СЃС‚Р°С‚СѓСЃРѕРј 'РћС‡С–РєСѓС”'.");

        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
            throw new InvalidOperationException($"РџРѕР·РёС†С–СЋ Р· Id '{itemId}' РЅРµ Р·РЅР°Р№РґРµРЅРѕ.");

        _items.Remove(item);
        UpdatedAt = DateTime.UtcNow;
    }

    public Money CalculateTotal()
    {
        if (!_items.Any())
            return new Money(0, "UAH");

        var currency = _items.First().UnitPrice.Currency;
        return _items.Aggregate(
            new Money(0, currency),
            (total, item) => total.Add(item.GetTotalPrice()));
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("РџС–РґС‚РІРµСЂРґРёС‚Рё РјРѕР¶РЅР° Р»РёС€Рµ Р·Р°РјРѕРІР»РµРЅРЅСЏ Р·С– СЃС‚Р°С‚СѓСЃРѕРј 'РћС‡С–РєСѓС”'.");
        if (!_items.Any())
            throw new InvalidOperationException("РќРµ РјРѕР¶РЅР° РїС–РґС‚РІРµСЂРґРёС‚Рё Р·Р°РјРѕРІР»РµРЅРЅСЏ Р±РµР· С‚РѕРІР°СЂС–РІ.");

        Status = OrderStatus.Confirmed;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new OrderConfirmedEvent(Id, CustomerId, CalculateTotal().Amount));
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Delivered || Status == OrderStatus.Cancelled)
            throw new InvalidOperationException($"РќРµ РјРѕР¶РЅР° СЃРєР°СЃСѓРІР°С‚Рё Р·Р°РјРѕРІР»РµРЅРЅСЏ Р·С– СЃС‚Р°С‚СѓСЃРѕРј '{Status}'.");

        Status = OrderStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsShipped()
    {
        if (Status != OrderStatus.Confirmed && Status != OrderStatus.Processing)
            throw new InvalidOperationException("Р’С–РґРїСЂР°РІРёС‚Рё РјРѕР¶РЅР° Р»РёС€Рµ РїС–РґС‚РІРµСЂРґР¶РµРЅРµ Р°Р±Рѕ РѕР±СЂРѕР±Р»СЋРІР°РЅРµ Р·Р°РјРѕРІР»РµРЅРЅСЏ.");

        Status = OrderStatus.Shipped;
        UpdatedAt = DateTime.UtcNow;
    }
}
