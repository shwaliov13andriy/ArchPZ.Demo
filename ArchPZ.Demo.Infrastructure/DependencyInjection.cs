namespace ArchPZ.Demo.Infrastructure;

using ArchPZ.Demo.Domain.Contracts;
using ArchPZ.Demo.Domain.OrderManagement.Repositories;
using ArchPZ.Demo.Domain.UserManagement.Repositories;
using ArchPZ.Demo.Infrastructure.Repositories;
using ArchPZ.Demo.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();

        services.AddScoped<ICustomerValidationService, CustomerValidationService>();

        return services;
    }
}
