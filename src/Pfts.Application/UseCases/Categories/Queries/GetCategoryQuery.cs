namespace Pfts.Application.UseCases.Categories.Queries;

using Microsoft.EntityFrameworkCore;
using Pfts.Application.UseCases.Categories.Models;
using Pfts.Domain.Exceptions;
using Pfts.Infrastructure.Persistance;

public record GetCategoryQuery(Guid Id) : IRequest<CategoryModel>;

internal sealed class GetCitiyQueryHandler(IAppDbContext dbContext)
    : IRequestHandler<GetCategoryQuery, CategoryModel>
{
    public async Task<CategoryModel> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await dbContext.Categories
            .Where(a => a.Id == request.Id)
            .Select(a => new CategoryModel
            {
                Id = a.Id,
                Color = a.Color,
                Name = a.Name,
            }).FirstOrDefaultAsync(cancellationToken);

        return result ?? throw new NotFoundException(nameof(CategoryModel), request.Id);
    }
}
