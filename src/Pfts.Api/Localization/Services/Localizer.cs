namespace Pfts.Api.Localization.Services;

using Microsoft.Extensions.Localization;
using Pfts.Api.Localization.Abstractions;

internal class Localizer : ILocalizer
{
    private readonly ILanguageResolver _languageResolver;

    private readonly IStringLocalizer _localizer;

    public string LanguageCode => _languageResolver.GetLanguageCode() ?? Options.DefaultCulture;

    public Options.LocalizationOptions Options { get; }

    public LocalizedString this[string name] => Localize(name);

    public Localizer(ILanguageResolver languageResolver, IStringLocalizerFactory factory, Options.LocalizationOptions options)
    {
        Options = options;
        _languageResolver = languageResolver;
        _localizer = factory.Create(options.ResourceSource);
    }

    public LocalizedString Localize(string key)
    {
        return _localizer.GetString(key);
    }
}
