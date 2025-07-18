/*namespace Pfts.Application.UseCases.Cities.Commands;

using Microsoft.EntityFrameworkCore;
using Pfts.Application.Common;
using Pfts.Domain.Exceptions;

public record DeleteCityCommand(Guid Id) : IRequest;

internal class DeleteCityCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteCityCommand>
{
    public async Task Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var deletedRows = await dbContext.Cities
            .Where(w => w.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (deletedRows == 0)
        {

            throw new NotFoundException(nameof(City), request.Id);
        }
    }
}
*/
