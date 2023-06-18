using ItemManagementSystem1.Data;
using ItemManagementSystem1.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemManagementSystem1.Repositories.CategoryRepository;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<Category>> GetAllCategory()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryById(int id)
    {
        return await _dbContext.Categories.FindAsync(id);
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        _dbContext.SaveChanges();
    }

    public void UpdateCategory(Category category, Category categoryToUpdate)
    {
        category.Id = categoryToUpdate.Id;
        category.Name = categoryToUpdate.Name;
        _dbContext.Categories.Update(category);
        _dbContext.SaveChanges();
    }

    public void RemoveCategory(Category category)
    {
        _dbContext.Categories.Remove(category);
        _dbContext.SaveChanges();
    }
}