namespace ArchPZ.Demo.Domain.Contracts;

public interface ICustomerValidationService
{
    Task<bool> CustomerExistsAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<string?> GetCustomerEmailAsync(Guid customerId, CancellationToken cancellationToken = default);
}
