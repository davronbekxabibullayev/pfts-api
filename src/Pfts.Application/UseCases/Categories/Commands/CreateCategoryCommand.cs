namespace Pfts.Application.UseCases.Categories.Commands;

using Pfts.Infrastructure.Persistance;
using Pfts.Domain.Models;
using Pfts.Application.Common;

public record CreateCategoryCommand : IRequest
{
    public string Name { get; set; } = default!;
    public string Color { get; set; } = default!;
    public bool IsDeleted { get; set; }
}

internal class CreateCategoryCommandHandler(
    IAppDbContext dbContext,
    ICurrentUser currentUser) : IRequestHandler<CreateCategoryCommand>
{
    public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = NewCategory(request);

        await dbContext.Categories.AddAsync(category, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Category NewCategory(CreateCategoryCommand request)
    {
        return new Category
        {
            Name = request.Name,
            Color = request.Color,
            UserId = currentUser.UserId,
            IsDeleted = request.IsDeleted
        };
    }
}
