using Microsoft.AspNetCore.Mvc;
using Moq;
using SOAProject.DTOs;
using SOAProject.Models;
using SOAProject.Services.AssetService;

namespace SOAProject.Controllers.Tests
{
    public class AssetControllerTests
    {
        private readonly Mock<IAssetService> _mockAssetService;
        private readonly AssetController _assetController;

        public AssetControllerTests()
        {
            _mockAssetService = new Mock<IAssetService>();
            _assetController = new AssetController(_mockAssetService.Object);
        }

        [Fact]
        public async Task GetAllAssets_ShouldReturnAllAssets()
        {
            var assets = new List<Asset>
            {
                new Asset { Id = 1, Name = "Asset 1", CategoryId = 1, SerialNr = "12345" },
                new Asset { Id = 2, Name = "Asset 2", CategoryId = 2, SerialNr = "67890" }
            };

            _mockAssetService.Setup(service => service.GetAllAssets())
                .ReturnsAsync(assets);

            var result = await _assetController.GetAllAssets();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAssets = Assert.IsAssignableFrom<IEnumerable<Asset>>(okResult.Value);
            Assert.Equal(assets.Count, returnedAssets.Count());
            Assert.Equal(assets, returnedAssets);
            _mockAssetService.Verify(service => service.GetAllAssets(), Times.Once);
        }

        [Fact]
        public async Task GetAssetById_ExistingId_ShouldReturnAsset()
        {
            var assetId = 1;
            var asset = new Asset { Id = assetId, Name = "Asset 1", CategoryId = 1, SerialNr = "12345" };

            _mockAssetService.Setup(service => service.GetAssetById(assetId))
                .ReturnsAsync(asset);

            var result = await _assetController.GetAssetById(assetId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAsset = Assert.IsType<Asset>(okResult.Value);
            Assert.Equal(asset, returnedAsset);
            _mockAssetService.Verify(service => service.GetAssetById(assetId), Times.Once);
        }

        [Fact]
        public async Task GetAssetById_NonExistingId_ShouldReturnNotFound()
        {
            var assetId = 1;

            _mockAssetService.Setup(service => service.GetAssetById(assetId))
                .ReturnsAsync((Asset)null);

            var result = await _assetController.GetAssetById(assetId);

            Assert.IsType<NotFoundResult>(result.Result);
            _mockAssetService.Verify(service => service.GetAssetById(assetId), Times.Once);
        }

        [Fact]
        public async Task CreateCategory_ValidItem_ShouldReturnCreatedAsset()
        {
            var item = new AssetDTO { Name = "Asset 1", CategoryId = 1, SerialNr = "12345" };
            var asset = new Asset { Id = 1, Name = "Asset 1", CategoryId = 1, SerialNr = "12345" };

            _mockAssetService.Setup(service => service.CreateAsset(It.IsAny<Asset>()));

            var result = await _assetController.CreateCategory(item);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedItem = Assert.IsType<AssetDTO>(okResult.Value);
            Assert.Equal(item, returnedItem);
        }

        [Fact]
        public async Task UpdateAssetAsync_ExistingIdAndMatchingAsset_ShouldReturnOkResult()
        {
            var assetId = 1;
            var asset = new Asset { Id = assetId, Name = "Asset 1", CategoryId = 1, SerialNr = "12345" };

            _mockAssetService.Setup(service => service.UpdateAssetAsync(assetId, asset))
                .ReturnsAsync("Updated successfully");

            var result = await _assetController.UpdateAssetAsync(assetId, asset);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var message = Assert.IsType<string>(okResult.Value);
            Assert.Equal("Updated successfully", message);
            _mockAssetService.Verify(service => service.UpdateAssetAsync(assetId, asset), Times.Once);
        }

        [Fact]
        public async Task UpdateAssetAsync_ExistingIdAndMismatchingAsset_ShouldReturnBadRequest()
        {
            var assetId = 1;
            var asset = new Asset { Id = assetId + 1, Name = "Asset 2", CategoryId = 2, SerialNr = "67890" };

            var result = await _assetController.UpdateAssetAsync(assetId, asset);

            Assert.IsType<BadRequestResult>(result);
            _mockAssetService.Verify(service => service.UpdateAssetAsync(assetId, asset), Times.Never);
        }

        [Fact]
        public async Task UpdateAssetAsync_NonExistingId_ShouldReturnNotFound()
        {
            var assetId = 1;
            var asset = new Asset { Id = assetId, Name = "Asset 1", CategoryId = 1, SerialNr = "12345" };

            _mockAssetService.Setup(service => service.UpdateAssetAsync(assetId, asset))
                .ThrowsAsync(new InvalidOperationException());

            var result = await _assetController.UpdateAssetAsync(assetId, asset);

            Assert.IsType<NotFoundResult>(result);
            _mockAssetService.Verify(service => service.UpdateAssetAsync(assetId, asset), Times.Once);
        }

        [Fact]
        public async Task DeleteAsset_ExistingId_ShouldReturnOkResult()
        {
            var assetId = 1;
            var asset = new Asset { Id = assetId, Name = "Asset 1", CategoryId = 1, SerialNr = "12345" };

            _mockAssetService.Setup(service => service.GetAssetById(assetId))
                .ReturnsAsync(asset);
            _mockAssetService.Setup(service => service.DeleteAsset(asset.Id))
                .ReturnsAsync("Deleted successfully");

            var result = await _assetController.DeleteAsset(assetId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var message = Assert.IsType<string>(okResult.Value);
            Assert.Equal("Deleted successfully", message);
            _mockAssetService.Verify(service => service.GetAssetById(assetId), Times.Once);
            _mockAssetService.Verify(service => service.DeleteAsset(asset.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsset_NonExistingId_ShouldReturnNotFound()
        {
            var assetId = 1;

            _mockAssetService.Setup(service => service.GetAssetById(assetId))
                .ReturnsAsync((Asset)null);

            var result = await _assetController.DeleteAsset(assetId);

            Assert.IsType<NotFoundResult>(result);
            _mockAssetService.Verify(service => service.GetAssetById(assetId), Times.Once);
            _mockAssetService.Verify(service => service.DeleteAsset(It.IsAny<int>()), Times.Never);
        }
    }
}