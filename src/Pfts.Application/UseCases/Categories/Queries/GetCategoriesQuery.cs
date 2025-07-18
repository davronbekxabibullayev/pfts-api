namespace Pfts.Application.UseCases.Categories.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pfts.Application.UseCases.Categories.Models;
using Pfts.Infrastructure.Persistance;

public record GetCategoriesQuery : IRequest<ICollection<CategoryModel>>;

internal sealed class GetCitiesQueryHandler(IAppDbContext dbContext)
    : IRequestHandler<GetCategoriesQuery, ICollection<CategoryModel>>
{
    public async Task<ICollection<CategoryModel>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var response = await dbContext.Categories
            .Select(a => new CategoryModel
            {
                Id = a.Id,
                Color = a.Color,
                Name = a.Name,
            })
            .ToListAsync(cancellationToken);

        return response;
    }
}
