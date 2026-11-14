namespace ArchPZ.Demo.Application.OrderManagement.Validators;

using FluentValidation;
using ArchPZ.Demo.Application.OrderManagement.Commands;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required");

        RuleFor(x => x.ShippingAddress)
            .NotEmpty()
            .WithMessage("Shipping address is required")
            .MinimumLength(10)
            .WithMessage("Shipping address must be at least 10 characters")
            .MaximumLength(100)
            .WithMessage("Shipping address must not exceed 100 characters");

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0)
            .WithMessage("Total amount must be greater than zero")
            .LessThanOrEqualTo(1_000_000)
            .WithMessage("Total amount must not exceed 1,000,000");
    }
}
