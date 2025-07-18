namespace Pfts.Api.Localization.Services;

using Microsoft.AspNetCore.Http;
using Pfts.Api.Localization.Abstractions;

internal class HttpHeaderLanguageResolver(IHttpContextAccessor contextAccessor) : ILanguageResolver
{
    public string? GetLanguageCode()
    {
        return contextAccessor.HttpContext?
            .Request
            .GetTypedHeaders()
            .AcceptLanguage
            .FirstOrDefault()?.Value
            .Value;
    }
}
