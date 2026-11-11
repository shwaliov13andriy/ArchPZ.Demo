namespace ArchPZ.Demo.Application;

using ArchPZ.Demo.Application.Mapping;
using ArchPZ.Demo.Application.OrderManagement.Factories;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddTransient<IOrderFactory, OrderFactory>();
        services.AddTransient<IOrderMapper, OrderMapper>();
        services.AddTransient<IUserMapper, UserMapper>();

        return services;
    }
}
