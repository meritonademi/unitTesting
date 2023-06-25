using Moq;
using SOAProject.Models;
using SOAProject.Repositories.CategoryRepository;

namespace SOAProject.Services.CategoryService.Tests
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_mockCategoryRepository.Object);
        }

        [Fact]
        public async Task GetAllCategory_ShouldReturnAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" }
            };

            _mockCategoryRepository.Setup(repo => repo.GetAllCategory())
                .ReturnsAsync(categories);

            // Act
            var result = await _categoryService.GetAllCategory();

            // Assert
            Assert.Equal(categories.Count, result.Count());
            Assert.Equal(categories, result);
            _mockCategoryRepository.Verify(repo => repo.GetAllCategory(), Times.Once);
        }

        [Fact]
        public async Task GetCategoryById_ExistingId_ShouldReturnCategory()
        {
            // Arrange
            var categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Category 1" };

            _mockCategoryRepository.Setup(repo => repo.GetCategoryById(categoryId))
                .ReturnsAsync(category);

            // Act
            var result = await _categoryService.GetCategoryById(categoryId);

            // Assert
            Assert.Equal(category, result);
            _mockCategoryRepository.Verify(repo => repo.GetCategoryById(categoryId), Times.Once);
        }

        [Fact]
        public async Task GetCategoryById_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            var categoryId = 1;

            _mockCategoryRepository.Setup(repo => repo.GetCategoryById(categoryId))
                .ReturnsAsync((Category)null);

            // Act
            var result = await _categoryService.GetCategoryById(categoryId);

            // Assert
            Assert.Null(result);
            _mockCategoryRepository.Verify(repo => repo.GetCategoryById(categoryId), Times.Once);
        }

        [Fact]
        public async Task CreateCategory_ShouldCreateNewCategory()
        {
            // Arrange
            var category = new Category { Name = "New Category" };

            _mockCategoryRepository.Setup(repo => repo.AddCategoryAsync(It.IsAny<Category>()));

            // Act
            var result = await _categoryService.CreateCategory(category);

            // Assert
            Assert.NotNull(result);
            _mockCategoryRepository.Verify(repo => repo.AddCategoryAsync(category), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryAsync_ExistingId_ShouldUpdateCategory()
        {
            // Arrange
            var categoryId = 1;
            var existingCategory = new Category { Id = categoryId, Name = "Category 1" };
            var updatedCategory = new Category { Id = categoryId, Name = "Updated Category" };

            _mockCategoryRepository.Setup(repo => repo.GetCategoryById(categoryId))
                .ReturnsAsync(existingCategory);
            _mockCategoryRepository.Setup(repo => repo.UpdateCategory(It.IsAny<Category>(), It.IsAny<Category>()));

            // Act
            var result = await _categoryService.UpdateCategoryAsync(categoryId, updatedCategory);

            // Assert
            Assert.Equal("Successfully update", result);
            _mockCategoryRepository.Verify(repo => repo.UpdateCategory(existingCategory, updatedCategory), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryAsync_NonExistingId_ShouldThrowArgumentException()
        {
            // Arrange
            var categoryId = 1;
            var updatedCategory = new Category { Id = categoryId, Name = "Updated Category" };

            _mockCategoryRepository.Setup(repo => repo.GetCategoryById(categoryId))
                .ReturnsAsync((Category)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _categoryService.UpdateCategoryAsync(categoryId, updatedCategory));
        }

        [Fact]
        public async Task DeleteCategory_ExistingId_ShouldDeleteCategory()
        {
            // Arrange
            var categoryId = 1;
            var existingCategory = new Category { Id = categoryId, Name = "Category 1" };

            _mockCategoryRepository.Setup(repo => repo.GetCategoryById(categoryId))
                .ReturnsAsync(existingCategory);
            _mockCategoryRepository.Setup(repo => repo.RemoveCategory(It.IsAny<Category>()));

            // Act
            var result = await _categoryService.DeleteCategory(categoryId);

            // Assert
            Assert.Equal("Successfully delete", result);
            _mockCategoryRepository.Verify(repo => repo.RemoveCategory(existingCategory), Times.Once);
        }

        [Fact]
        public async Task DeleteCategory_NonExistingId_ShouldThrowArgumentException()
        {
            // Arrange
            var categoryId = 1;

            _mockCategoryRepository.Setup(repo => repo.GetCategoryById(categoryId))
                .ReturnsAsync((Category)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _categoryService.DeleteCategory(categoryId));
        }
    }
}