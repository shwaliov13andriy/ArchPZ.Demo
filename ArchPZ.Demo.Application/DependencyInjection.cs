namespace ArchPZ.Demo.Application;

using FluentValidation;
using MediatR;
using ArchPZ.Demo.Application.Common.Behaviours;
using ArchPZ.Demo.Application.Mapping;
using ArchPZ.Demo.Application.OrderManagement.Factories;
using ArchPZ.Demo.Application.OrderManagement.Validators;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddTransient<IOrderFactory, OrderFactory>();
        services.AddTransient<IOrderMapper, OrderMapper>();
        services.AddTransient<IUserMapper, UserMapper>();

        return services;
    }
}
