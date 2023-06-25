using Microsoft.AspNetCore.Mvc;
using Moq;
using SOAProject.Controllers;
using SOAProject.Models;
using SOAProject.Services.CategoryService;

public class CategoryControllerTests
{
    private readonly Mock<ICategoryService> _mockCategoryService;
    private readonly CategoryController _categoryController;

    public CategoryControllerTests()
    {
        _mockCategoryService = new Mock<ICategoryService>();
        _categoryController = new CategoryController(_mockCategoryService.Object);
    }

    [Fact]
    public async Task GetAllCategories_ShouldReturnOkResultWithItems()
    {
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Category 1" },
            new Category { Id = 2, Name = "Category 2" }
        };

        _mockCategoryService.Setup(service => service.GetAllCategory())
            .ReturnsAsync(categories);

        var result = await _categoryController.GetAllCategories();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedItems = Assert.IsAssignableFrom<IEnumerable<Category>>(okResult.Value);
        Assert.Equal(categories, returnedItems);
        _mockCategoryService.Verify(service => service.GetAllCategory(), Times.Once);
    }

    [Fact]
    public async Task GetCategoryById_ExistingId_ShouldReturnOkResultWithItem()
    {
        var categoryId = 1;
        var category = new Category { Id = categoryId, Name = "Category 1" };

        _mockCategoryService.Setup(service => service.GetCategoryById(categoryId))
            .ReturnsAsync(category);

        var result = await _categoryController.GetCategoryById(categoryId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedItem = Assert.IsType<Category>(okResult.Value);
        Assert.Equal(category, returnedItem);
    }

    [Fact]
    public async Task GetCategoryById_NonExistingId_ShouldReturnNotFound()
    {
        var categoryId = 1;

        _mockCategoryService.Setup(service => service.GetCategoryById(categoryId))
            .ReturnsAsync((Category)null);

        var result = await _categoryController.GetCategoryById(categoryId);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateCategory_ValidItem_ShouldReturnOkResultWithCreatedItem()
    {
        var item = new Category { Id = 1, Name = "New Category" };

        var result = await _categoryController.CreateCategory(item);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedItem = Assert.IsType<Category>(okResult.Value);
        Assert.Equal(item, returnedItem);
    }

    [Fact]
    public async Task UpdateCategoryAsync_ExistingIdAndMatchingCategory_ShouldReturnOkResultWithMessage()
    {
        var categoryId = 1;
        var category = new Category { Id = categoryId, Name = "Updated Category" };

        _mockCategoryService.Setup(service => service.UpdateCategoryAsync(categoryId, category))
            .ReturnsAsync("Category updated");

        var result = await _categoryController.UpdateCategoryAsync(categoryId, category);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var message = Assert.IsType<string>(okResult.Value);
        Assert.Equal("Category updated", message);
    }

    [Fact]
    public async Task UpdateCategoryAsync_ExistingIdAndNonMatchingCategory_ShouldReturnBadRequest()
    {
        var categoryId = 1;
        var category = new Category { Id = 2, Name = "Updated Category" };

        var result = await _categoryController.UpdateCategoryAsync(categoryId, category);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task UpdateCategoryAsync_NonExistingId_ShouldReturnNotFound()
    {
        var categoryId = 1;
        var category = new Category { Id = categoryId, Name = "Updated Category" };

        _mockCategoryService.Setup(service => service.UpdateCategoryAsync(categoryId, category))
            .ThrowsAsync(new InvalidOperationException());

        var result = await _categoryController.UpdateCategoryAsync(categoryId, category);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteCategory_ExistingId_ShouldReturnOkResultWithMessage()
    {
        var categoryId = 1;
        var category = new Category { Id = categoryId, Name = "Category 1" };

        _mockCategoryService.Setup(service => service.GetCategoryById(categoryId))
            .ReturnsAsync(category);
        _mockCategoryService.Setup(service => service.DeleteCategory(category.Id))
            .ReturnsAsync("Category deleted");

        var result = await _categoryController.DeleteCategory(categoryId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var message = Assert.IsType<string>(okResult.Value);
        Assert.Equal("Category deleted", message);
       }

    [Fact]
    public async Task DeleteCategory_NonExistingId_ShouldReturnNotFound()
    {
        var categoryId = 1;

        _mockCategoryService.Setup(service => service.GetCategoryById(categoryId))
            .ReturnsAsync((Category)null);

        var result = await _categoryController.DeleteCategory(categoryId);

        Assert.IsType<NotFoundResult>(result);
       }
}
