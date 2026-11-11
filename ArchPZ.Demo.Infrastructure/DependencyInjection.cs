namespace ArchPZ.Demo.Infrastructure;

using ArchPZ.Demo.Application.Contracts;
using ArchPZ.Demo.Application.Events;
using ArchPZ.Demo.Application.OrderManagement.Events;
using ArchPZ.Demo.Application.OrderManagement.Events.Handlers;
using ArchPZ.Demo.Domain.Contracts;
using ArchPZ.Demo.Domain.OrderManagement.Repositories;
using ArchPZ.Demo.Domain.UserManagement.Repositories;
using ArchPZ.Demo.Infrastructure.Repositories;
using ArchPZ.Demo.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, InMemoryOrderRepository>();
        services.AddScoped<IUserRepository, InMemoryUserRepository>();

        services.AddScoped<ICustomerValidationService, CustomerValidationService>();

        services.AddSingleton<IEventBus, InProcessEventBus>();
        services.AddTransient<IEventHandler<OrderCreatedEvent>, OrderCreatedNotificationHandler>();
        services.AddTransient<IEventHandler<OrderCreatedEvent>, OrderCreatedAnalyticsHandler>();

        services.AddSingleton<ILoggerService, ConsoleLoggerService>();

        return services;
    }
}
