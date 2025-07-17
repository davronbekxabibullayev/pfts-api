namespace Pfts.Infrastucture.Persistence.EntityFramework.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Pfts.Application.Common;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationPersistence(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        var dataSource = dataSourceBuilder.Build();

        services.AddDbContext<IAppDbContext, AppDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(dataSource,
                options =>
                {
                    options.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name);
                    options.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                })
            .EnableSensitiveDataLogging();
        });

        return services;
    }
}
