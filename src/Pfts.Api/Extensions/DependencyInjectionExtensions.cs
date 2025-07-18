namespace Pfts.Api.Extensions;

using System.Reflection;
using Npgsql;
using Pfts.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pfts.Application.Common;
using Pfts.Infrastructure.Persistance.EntityFramework;
using Pfts.Infrastructure.Persistance;
using Polly;
internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationApi(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddApplicationApiCORS();

        services.AddTransient<IAuthService, AuthService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());


        return services;
    }

    public static IServiceCollection AddAppDbContext(this IServiceCollection services, string connectionString)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.EnableDynamicJson();
        var dataSource = dataSourceBuilder.Build();

        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(dataSource,
                options =>
                {
                    options.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                    options.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                })
            .EnableSensitiveDataLogging();
        }, ServiceLifetime.Scoped);

        services.AddScoped<IAppDbContext, AppDbContext>();

        return services;
    }

    public static void MigrateDbContext<TContext>(this IApplicationBuilder app, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetRequiredService<TContext>();

        try
        {
            logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

            var retries = 10;
            var retry = Policy.Handle<NpgsqlException>()
                    .WaitAndRetry(retryCount: retries,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        onRetry: (exception, timeSpan, retry, ctx) =>
                        {
                            logger.LogWarning(exception, "[{Prefix}] Exception {ExceptionType} with message {Message} detected on attempt {Retry} of {Retries}", nameof(TContext), exception.GetType().Name, exception.Message, retry, retries);
                        });

            retry.Execute(() => InvokeSeeder(seeder, context, services));

            logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
        }
    }

    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
        where TContext : DbContext
    {
        seeder(context, services);
    }
}
