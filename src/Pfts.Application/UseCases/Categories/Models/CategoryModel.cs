namespace Pfts.Application.UseCases.Categories.Models;

using System;

public class CategoryModel
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Color { get; set; } = default!;
    public Guid UserId { get; set; }
}
