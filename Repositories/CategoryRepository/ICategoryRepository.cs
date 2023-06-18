using ItemManagementSystem1.Models;

namespace ItemManagementSystem1.Repositories.CategoryRepository;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategory();
    Task<Category> GetCategoryById(int id);
    Task AddCategoryAsync(Category category);
    void UpdateCategory(Category category);
    void RemoveCategory(Category category);
}