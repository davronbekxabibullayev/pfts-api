namespace Pfts.Application.UseCases.Categories.Commands;

using Microsoft.EntityFrameworkCore;
using Pfts.Infrastructure.Persistance;

public record DeleteCategoryCommand(Guid Id) : IRequest;

internal class DeleteCategoryCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var deletedRows = await dbContext.Categories
            .Where(w => w.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);
    }
}
