/*namespace Pfts.Application.UseCases.Cities.Commands;

using Pfts.Application.Common;

public record CreateCityCommand : IRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public required Guid RegionId { get; set; }
    public required string Code { get; set; }
    public string? StateCode { get; set; }
}

internal class CreateCityCommandHandler(IAppDbContext dbContext) : IRequestHandler<CreateCityCommand>
{
    public async Task Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var city = NewCity(request);

        await dbContext.Cities.AddAsync(city, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static City NewCity(CreateCityCommand request)
    {
        return new City
        {
            Name = request.Name,
            NameRu = request.NameRu,
            NameUz = request.NameUz,
            NameKa = request.NameKa,
            RegionId = request.RegionId,
            Code = request.Code,
            StateCode = request.StateCode
        };
    }
}
*/
