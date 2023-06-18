using ItemManagementSystem1.Models;
using ItemManagementSystem1.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace ItemManagementSystem1.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
    {
        var items = await _categoryService.GetAllCategory();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryById(int id)
    {
        var item = await _categoryService.GetCategoryById(id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(Category item)
    {
        await _categoryService.CreateCategory(item);
        return Ok(item);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCategoryAsync(int id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }

        try
        {
            var message = await _categoryService.UpdateCategoryAsync(id, category);
            return Ok(message);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var item = await _categoryService.GetCategoryById(id);
        if (item == null)
        {
            return NotFound();
        }

        var message = await _categoryService.DeleteCategory(item.Id);
        return Ok(message);
    }
}