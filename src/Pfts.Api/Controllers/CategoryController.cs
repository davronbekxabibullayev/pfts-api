namespace Pfts.Api.Controllers;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pfts.Api.Models.Category;
using Pfts.Application.UseCases.Categories.Commands;
using Pfts.Application.UseCases.Categories.Queries;

[Route("api/categories")]
[ApiController]
[Authorize]
public class CategoryController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Получение списка всех категорий
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var request = new GetCategoriesQuery();

        var response = await mediator.Send(request);

        return Ok(response);
    }

    /// <summary>
    /// Создание новой категории 
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        await mediator.Send(new CreateCategoryCommand
        {
            Name = request.Name,
            Color = request.Color,
            IsDeleted = request.IsDeleted
        });

        return Ok();
    }

    /// <summary>
    /// Удаление категории по идентификатору 
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var request = new DeleteCategoryCommand(id);

        await mediator.Send(request);

        return Ok();
    }

    /// <summary>
    /// Получение информации о категории по её идентификатору 
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        var request = new GetCategoryQuery(id);

        var response = await mediator.Send(request);

        return Ok();
    }
}

