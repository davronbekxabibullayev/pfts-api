namespace Pfts.Api.Localization.Abstractions;

using Microsoft.Extensions.Localization;

public interface ILocalizer
{
    public string LanguageCode { get; }
    public Options.LocalizationOptions Options { get; }
    public LocalizedString this[string name] { get; }
    public LocalizedString Localize(string key);
}
