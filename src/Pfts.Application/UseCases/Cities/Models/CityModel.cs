namespace Pfts.Application.UseCases.Cities.Models;

using Pfts.Application.Common.Models;

public class CityModel : LocalizableName
{
    public required Guid Id { get; set; }
    public Guid RegionId { get; set; }

    public required string Code { get; set; }

    public string? StateCode { get; set; }
}
