namespace Pfts.Application.Common.Models;
public abstract class LocalizableName : ILocalizableName
{
    /// <summary>
    /// Наименование на узбекском
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Наименование на русском
    /// </summary>
    public required string NameRu { get; set; }

    /// <summary>
    /// Наименование на узбекском кириллице
    /// </summary>
    public string? NameUz { get; set; }

    /// <summary>
    /// Наименование на каракалпакском
    /// </summary>
    public string? NameKa { get; set; }
}
