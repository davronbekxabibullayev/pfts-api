namespace Pfts.Api.Localization.Extensions;

using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pfts.Api.Localization.Abstractions;
using Pfts.Api.Localization.Options;
using Pfts.Api.Localization.Services;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddCoreLocalization(this IServiceCollection services, Action<LocalizationOptions> options)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");

        var localizationOptions = new LocalizationOptions();
        services.AddSingleton(localizationOptions);
        options?.Invoke(localizationOptions);
        services.AddScoped<ILanguageResolver, HttpHeaderLanguageResolver>();
        services.AddScoped<ILocalizer, Localizer>();
        services.Configure((RequestLocalizationOptions requestOptions) =>
        {
            var list = localizationOptions.SupportedLanguages.Select((i) => new CultureInfo(i)).ToList();
            requestOptions.DefaultRequestCulture = new RequestCulture(new CultureInfo(localizationOptions.DefaultCulture), new CultureInfo(localizationOptions.DefaultCulture));
            requestOptions.SupportedCultures = list;
            requestOptions.SupportedUICultures = list;
            requestOptions.RequestCultureProviders.Clear();
            requestOptions.RequestCultureProviders.Insert(0, new AcceptLanguageHeaderRequestCultureProvider());
        });
        return services;
    }

    public static IServiceCollection AddDevhubLocalization(this IServiceCollection services, IConfiguration config)
    {
        services.AddCoreLocalization(options =>
        {
            var supportedLanguages = config.GetSection("Localization:SupportedCultures").Get<string[]>();
            var defaultCulture = config.GetSection("Localization:DefaultCulture").Get<string>();
            if (supportedLanguages != null)
            {
                options.SupportedLanguages = supportedLanguages;
            }
            if (!string.IsNullOrEmpty(defaultCulture))
            {
                options.DefaultCulture = defaultCulture;
            }
            options.ResourceSource = typeof(Resource);
        });

        return services;
    }
}
