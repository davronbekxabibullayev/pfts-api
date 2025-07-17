namespace Pfts.Api.Models.Cities;

public class UpdateCityRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public required Guid RegionId { get; set; }
    public required string Code { get; set; }
    public string? StateCode { get; set; }
}
