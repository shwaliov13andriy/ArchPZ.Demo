namespace ArchPZ.Demo.Infrastructure;

using ArchPZ.Demo.Domain.Contracts;
using ArchPZ.Demo.Domain.OrderManagement.Repositories;
using ArchPZ.Demo.Domain.UserManagement.Repositories;
using ArchPZ.Demo.Infrastructure.Repositories;
using ArchPZ.Demo.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Модуль реєстрації залежностей для Infrastructure шару.
/// Використовується у Composition Root (WebApi/Program.cs).
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Реєстрація репозиторіїв як Singleton (In-Memory сховище)
        services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();

        // Реєстрація сервісу взаємодії між Bounded Contexts
        services.AddScoped<ICustomerValidationService, CustomerValidationService>();

        return services;
    }
}
