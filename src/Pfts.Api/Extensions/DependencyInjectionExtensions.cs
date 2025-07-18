namespace Pfts.Api.Extensions;

using System.Reflection;
using Pfts.Api.Services;
using Pfts.Application.Common;

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
}
