namespace ArchPZ.Demo.Domain.OrderManagement.Entities;

using ArchPZ.Demo.Domain.Common;
using ArchPZ.Demo.Domain.OrderManagement.Enums;
using ArchPZ.Demo.Domain.OrderManagement.Events;
using ArchPZ.Demo.Domain.OrderManagement.ValueObjects;

/// <summary>
/// Aggregate Root — Замовлення.
/// Інкапсулює всю бізнес-логіку управління замовленнями.
/// </summary>
public class Order : AggregateRoot
{
    private readonly List<OrderItem> _items = new();

    public Guid CustomerId { get; private set; }
    public Address ShippingAddress { get; private set; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public string? Notes { get; private set; }

    // Для ORM
    private Order() : base()
    {
        ShippingAddress = null!;
    }

    /// <summary>
    /// Фабричний метод створення замовлення
    /// </summary>
    public static Order Create(Guid customerId, Address shippingAddress, string? notes = null)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("Ідентифікатор клієнта не може бути порожнім.", nameof(customerId));

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

    /// <summary>
    /// Додає позицію до замовлення
    /// </summary>
    public void AddItem(Guid productId, string productName, int quantity, Money unitPrice)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Можна додавати товари лише до замовлення зі статусом 'Очікує'.");

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

    /// <summary>
    /// Видаляє позицію із замовлення
    /// </summary>
    public void RemoveItem(Guid itemId)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Можна видаляти товари лише із замовлення зі статусом 'Очікує'.");

        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
            throw new InvalidOperationException($"Позицію з Id '{itemId}' не знайдено.");

        _items.Remove(item);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Обчислює загальну суму замовлення
    /// </summary>
    public Money CalculateTotal()
    {
        if (!_items.Any())
            return new Money(0, "UAH");

        var currency = _items.First().UnitPrice.Currency;
        return _items.Aggregate(
            new Money(0, currency),
            (total, item) => total.Add(item.GetTotalPrice()));
    }

    /// <summary>
    /// Підтверджує замовлення
    /// </summary>
    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Підтвердити можна лише замовлення зі статусом 'Очікує'.");
        if (!_items.Any())
            throw new InvalidOperationException("Не можна підтвердити замовлення без товарів.");

        Status = OrderStatus.Confirmed;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new OrderConfirmedEvent(Id, CustomerId, CalculateTotal().Amount));
    }

    /// <summary>
    /// Скасовує замовлення
    /// </summary>
    public void Cancel()
    {
        if (Status == OrderStatus.Delivered || Status == OrderStatus.Cancelled)
            throw new InvalidOperationException($"Не можна скасувати замовлення зі статусом '{Status}'.");

        Status = OrderStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Переводить замовлення у статус "Доставлено"
    /// </summary>
    public void MarkAsShipped()
    {
        if (Status != OrderStatus.Confirmed && Status != OrderStatus.Processing)
            throw new InvalidOperationException("Відправити можна лише підтверджене або оброблюване замовлення.");

        Status = OrderStatus.Shipped;
        UpdatedAt = DateTime.UtcNow;
    }
}
