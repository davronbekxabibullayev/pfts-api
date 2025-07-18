namespace Pfts.Application.UseCases.Categories.Commands;

using System;
using System.Threading.Tasks;
using Pfts.Domain.Exceptions;
using Pfts.Domain.Models;
using Pfts.Infrastructure.Persistance;

public record UpdateCategoryCommand(
   Guid Id,
   Guid UserId,
   string Color,
   string Name) : IRequest;

internal class UpdateCategoryCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await GetCategoryAsync(request.Id)
            ?? throw new NotFoundException(nameof(Category), request.Id);

        mapper.Map(request, category);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<Category?> GetCategoryAsync(Guid id)
    {
        return dbContext.Categories
                    .AsTracking()
                    .FirstOrDefaultAsync(w => w.Id == id);
    }
}
