namespace Pfts.Application.UseCases.Cities.Commands;

using Microsoft.EntityFrameworkCore;
using Pfts.Application.Common;
using Pfts.Domain.Exceptions;
using Pfts.Domain.Models;

public record UpdateCityCommand(
   Guid Id,
   Guid RegionId,
   string Name,
   string NameRu,
   string? NameUz,
   string? NameKa,
   string Code,
   string? StateCode) : IRequest;

internal class UpdateCityCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateCityCommand>
{
    public async Task Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        var city = await GetCityAsync(request.Id)
            ?? throw new NotFoundException(nameof(City), request.Id);

        mapper.Map(request, city);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<City?> GetCityAsync(Guid id)
    {
        return dbContext.Cities
                    .AsTracking()
                    .FirstOrDefaultAsync(w => w.Id == id);
    }
}
