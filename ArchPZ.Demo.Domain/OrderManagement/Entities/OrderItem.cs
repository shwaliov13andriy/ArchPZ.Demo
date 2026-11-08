namespace ArchPZ.Demo.Domain.OrderManagement.Entities;

using ArchPZ.Demo.Domain.Common;
using ArchPZ.Demo.Domain.OrderManagement.ValueObjects;

/// <summary>
/// Entity — Елемент замовлення (позиція).
/// Належить до Aggregate Root "Order".
/// </summary>
public class OrderItem : Entity
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }

    // Для ORM
    private OrderItem() : base()
    {
        ProductName = string.Empty;
        UnitPrice = new Money(0, "UAH");
    }

    public OrderItem(Guid productId, string productName, int quantity, Money unitPrice)
        : base()
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Назва продукту не може бути порожньою.", nameof(productName));
        if (quantity <= 0)
            throw new ArgumentException("Кількість повинна бути більше нуля.", nameof(quantity));

        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice));
    }

    /// <summary>
    /// Обчислює загальну вартість позиції
    /// </summary>
    public Money GetTotalPrice() => UnitPrice.Multiply(Quantity);

    /// <summary>
    /// Оновлює кількість товару в позиції
    /// </summary>
    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("Кількість повинна бути більше нуля.", nameof(newQuantity));

        Quantity = newQuantity;
        UpdatedAt = DateTime.UtcNow;
    }
}
