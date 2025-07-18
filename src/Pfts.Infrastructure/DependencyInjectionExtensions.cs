namespace Pfts.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pfts.Infrastructure.Persistance.EntityFramework.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddApplicationPersistance(configuration);

        return services;
    }
}
