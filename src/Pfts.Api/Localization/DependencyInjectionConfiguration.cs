namespace Pfts.Api.Localization;

using global::FluentValidation;
using Microsoft.AspNetCore.Builder;

public static class DependencyInjectionConfiguration
{
    public static IApplicationBuilder UseLocalizationFluentValidation(this IApplicationBuilder app)
    {
        ValidatorOptions.Global.LanguageManager = new ApplicationLanguageManager();

        return app;
    }
}
