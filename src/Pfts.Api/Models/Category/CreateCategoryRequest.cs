namespace Pfts.Api.Models.Category;

public sealed class CreateCategoryRequest
{
    public string Name { get; set; } = default!;
    public string Color { get; set; } = default!;
    public bool IsDeleted { get; set; }
}

