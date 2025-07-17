namespace Pfts.Api.Options;

public class LocalizationOptions
{
    public string[] SupportedLanguages { get; set; } = Array.Empty<string>();

    public string DefaultCulture { get; set; } = "ru";

    public Type ResourceSource { get; set; } = default!;
}
