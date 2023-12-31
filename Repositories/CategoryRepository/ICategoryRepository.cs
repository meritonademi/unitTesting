using SOAProject.Models;

namespace SOAProject.Repositories.CategoryRepository;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategory();
    Task<Category> GetCategoryById(int id);
    Task AddCategoryAsync(Category category);
    void UpdateCategory(Category category,Category categoryToUpdate);
    void RemoveCategory(Category category);
}