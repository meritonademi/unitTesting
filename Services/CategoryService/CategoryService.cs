using SOAProject.Models;
using SOAProject.Repositories.CategoryRepository;

namespace SOAProject.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetAllCategory()
    {
        var items = await _categoryRepository.GetAllCategory();
        return items;
    }

    public async Task<Category> GetCategoryById(int id)
    {
        var item = await _categoryRepository.GetCategoryById(id);
        return item;
    }

    public async Task<Category> CreateCategory(Category item)
    {
        try
        {
            _categoryRepository.AddCategoryAsync(item);
            return item;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> UpdateCategoryAsync(int id, Category category)
    {
        try
        {
            var item = await _categoryRepository.GetCategoryById(id);
            if (item == null)
            {
                throw new ArgumentException("Category not found.");
            }

            _categoryRepository.UpdateCategory(item, category);
            return "Successfully update";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> DeleteCategory(int id)
    {
        var item = await _categoryRepository.GetCategoryById(id);
        if (item == null)
        {
            throw new ArgumentException("Category not found.");
        }

        _categoryRepository.RemoveCategory(item);
        return "Successfully delete";
    }
}