namespace Pfts.Api.Localization;

using global::FluentValidation.Resources;

public class ApplicationLanguageManager : LanguageManager
{
    public ApplicationLanguageManager()
    {
        AddTranslation("ru", LocalizationKeys.NotNullValidator, "'{PropertyName}' должен быть указан.");
        AddTranslation("uz", LocalizationKeys.NotNullValidator, "'{PropertyName}' belgilangan bo'lishi shart.");
        AddTranslation("en", LocalizationKeys.NotNullValidator, "'{PropertyName}' is required.");
        AddTranslation("ka", LocalizationKeys.NotNullValidator, "'{PropertyName}' белгиленүүгө тийиш.");
    }
}
