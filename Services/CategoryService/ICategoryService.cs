using SOAProject.Models;

namespace SOAProject.Services.CategoryService;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategory();
    Task<Category> GetCategoryById(int id);
    Task<Category> CreateCategory(Category item);
    Task<string> UpdateCategoryAsync(int id, Category category);
    Task<string> DeleteCategory(int id);
}