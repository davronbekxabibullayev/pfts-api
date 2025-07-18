namespace Pfts.Api.Localization.Extensions;

using Microsoft.AspNetCore.Builder;

public static class ConfigurePipelineExtensions
{
    public static void UseDevhubLocalization(this IApplicationBuilder app)
    {
        app.UseRequestLocalization();
    }
}
