using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOAProject.Models;
using SOAProject.Repositories.AssetRepository;
using Xunit;

namespace SOAProject.Services.AssetService.Tests
{
    public class AssetServiceTests
    {
        private readonly Mock<IAssetRepository> _mockAssetRepository;
        private readonly AssetService _assetService;

        public AssetServiceTests()
        {
            _mockAssetRepository = new Mock<IAssetRepository>();
            _assetService = new AssetService(_mockAssetRepository.Object);
        }

        [Fact]
        public async Task GetAllAssets_ShouldReturnAllAssets()
        {
            // Arrange
            var assets = new List<Asset>
            {
                new Asset { Id = 1, Name = "Asset 1" },
                new Asset { Id = 2, Name = "Asset 2" }
            };

            _mockAssetRepository.Setup(repo => repo.GetAllAssets())
                .ReturnsAsync(assets);

            // Act
            var result = await _assetService.GetAllAssets();

            // Assert
            Assert.Equal(assets.Count, result.Count());
            Assert.Equal(assets, result);
            _mockAssetRepository.Verify(repo => repo.GetAllAssets(), Times.Once);
        }

        [Fact]
        public async Task GetAssetById_ExistingId_ShouldReturnAsset()
        {
            // Arrange
            var assetId = 1;
            var asset = new Asset { Id = assetId, Name = "Asset 1" };

            _mockAssetRepository.Setup(repo => repo.GetAssetById(assetId))
                .ReturnsAsync(asset);

            // Act
            var result = await _assetService.GetAssetById(assetId);

            // Assert
            Assert.Equal(asset, result);
            _mockAssetRepository.Verify(repo => repo.GetAssetById(assetId), Times.Once);
        }

        [Fact]
        public async Task GetAssetById_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            var assetId = 1;

            _mockAssetRepository.Setup(repo => repo.GetAssetById(assetId))
                .ReturnsAsync((Asset)null);

            // Act
            var result = await _assetService.GetAssetById(assetId);

            // Assert
            Assert.Null(result);
            _mockAssetRepository.Verify(repo => repo.GetAssetById(assetId), Times.Once);
        }

        [Fact]
        public async Task CreateAsset_ShouldCreateNewAsset()
        {
            // Arrange
            var asset = new Asset { Name = "New Asset" };

            _mockAssetRepository.Setup(repo => repo.AddAssetAsync(It.IsAny<Asset>()));

            // Act
            var result = await _assetService.CreateAsset(asset);

            // Assert
            Assert.NotNull(result);
            _mockAssetRepository.Verify(repo => repo.AddAssetAsync(asset), Times.Once);
        }

        [Fact]
        public async Task UpdateAssetAsync_ExistingId_ShouldUpdateAsset()
        {
            // Arrange
            var assetId = 1;
            var existingAsset = new Asset { Id = assetId, Name = "Asset 1" };
            var updatedAsset = new Asset { Id = assetId, Name = "Updated Asset" };

            _mockAssetRepository.Setup(repo => repo.GetAssetById(assetId))
                .ReturnsAsync(existingAsset);
            _mockAssetRepository.Setup(repo => repo.UpdateAsset(It.IsAny<Asset>(), It.IsAny<Asset>()));

            // Act
            var result = await _assetService.UpdateAssetAsync(assetId, updatedAsset);

            // Assert
            Assert.Equal("Successfully update", result);
            _mockAssetRepository.Verify(repo => repo.UpdateAsset(existingAsset, updatedAsset), Times.Once);
        }

        [Fact]
        public async Task UpdateAssetAsync_NonExistingId_ShouldThrowArgumentException()
        {
            // Arrange
            var assetId = 1;
            var updatedAsset = new Asset { Id = assetId, Name = "Updated Asset" };

            _mockAssetRepository.Setup(repo => repo.GetAssetById(assetId))
                .ReturnsAsync((Asset)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _assetService.UpdateAssetAsync(assetId, updatedAsset));
        }

        [Fact]
        public async Task DeleteAsset_ExistingId_ShouldDeleteAsset()
        {
            // Arrange
            var assetId = 1;
            var existingAsset = new Asset { Id = assetId, Name = "Asset 1" };

            _mockAssetRepository.Setup(repo => repo.GetAssetById(assetId))
                .ReturnsAsync(existingAsset);
            _mockAssetRepository.Setup(repo => repo.RemoveAsset(It.IsAny<Asset>()));

            // Act
            var result = await _assetService.DeleteAsset(assetId);

            // Assert
            Assert.Equal("Successfully delete", result);
            _mockAssetRepository.Verify(repo => repo.RemoveAsset(existingAsset), Times.Once);
        }

        [Fact]
        public async Task DeleteAsset_NonExistingId_ShouldThrowArgumentException()
        {
            // Arrange
            var assetId = 1;

            _mockAssetRepository.Setup(repo => repo.GetAssetById(assetId))
                .ReturnsAsync((Asset)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _assetService.DeleteAsset(assetId));
        }
    }
}
