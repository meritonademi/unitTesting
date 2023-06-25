using Microsoft.EntityFrameworkCore;
using SOAProject.Data;
using SOAProject.Models;
using SOAProject.Repositories.CategoryRepository;

namespace SOAProject.Tests
{
    public class CategoryRepositoryTests : IDisposable
    {
        private readonly DbContextOptions<AppDbContext> _options;
        private readonly AppDbContext _dbContext;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).EnableSensitiveDataLogging().Options;

            _dbContext = new AppDbContext(_options);
            _categoryRepository = new CategoryRepository(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Fact]
        public async Task GetAllCategory_ShouldReturnAllCategories()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" },
                new Category { Id = 3, Name = "Category 3" }
            };
            _dbContext.Categories.AddRange(categories);
            _dbContext.SaveChanges();

            var result = await _categoryRepository.GetAllCategory();

            Assert.Equal(categories.Count(), result.Count());
        }

        [Fact]
        public async Task GetCategoryById_ExistingId_ShouldReturnCategory()
        {
            var category = new Category { Id = 1, Name = "Category 1" };
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            var result = await _categoryRepository.GetCategoryById(category.Id);

            Assert.NotNull(result);
            Assert.Equal(category.Id, result.Id);
        }

        [Fact]
        public async Task GetCategoryById_NonExistingId_ShouldReturnNull()
        {
            var result = await _categoryRepository.GetCategoryById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddCategoryAsync_ShouldAddCategoryToDatabase()
        {
            var category = new Category { Id = 1, Name = "Category 1" };

            await _categoryRepository.AddCategoryAsync(category);

            var result = await _dbContext.Categories.FindAsync(category.Id);
            Assert.NotNull(result);
            Assert.Equal(category.Id, result.Id);
            Assert.Equal(category.Name, result.Name);
        }

        [Fact]
        public async Task UpdateCategory_ShouldUpdateCategoryInDatabase()
        {
            var existingCategory = new Category { Id = 1, Name = "Category 1" };
            _dbContext.Categories.Add(existingCategory);
            _dbContext.SaveChanges();

            var updatedCategory = new Category { Id = 1, Name = "Updated Category 1" };

            _categoryRepository.UpdateCategory(existingCategory, updatedCategory);

            var result = await _dbContext.Categories.FindAsync(existingCategory.Id);
            Assert.NotNull(result);
            Assert.Equal(updatedCategory.Name, result.Name);
        }

        [Fact]
        public async Task RemoveCategory_ShouldRemoveCategoryFromDatabase()
        {
            var category = new Category { Id = 1, Name = "Category 1" };
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            _categoryRepository.RemoveCategory(category);

            var result = await _dbContext.Categories.FindAsync(category.Id);
            Assert.Null(result);
        }
    }
}