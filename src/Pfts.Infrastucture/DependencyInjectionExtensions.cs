namespace Pfts.Infrastucture;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pfts.Infrastucture.Persistence.EntityFramework.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationPersistence(configuration);

        return services;
    }
}
