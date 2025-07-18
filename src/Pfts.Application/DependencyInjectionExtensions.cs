namespace Pfts.Application;

using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Pfts.Application.Common.Behaviors;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services
            .AddFluentValidationAutoValidation(options => options.DisableDataAnnotationsValidation = true)
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
