namespace ArchPZ.Demo.Application.DTOs;

public record OrderDto
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public string Status { get; init; } = string.Empty;
    public string ShippingAddress { get; init; } = string.Empty;
    public decimal TotalAmount { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public IReadOnlyList<OrderItemDto> Items { get; init; } = [];
}
