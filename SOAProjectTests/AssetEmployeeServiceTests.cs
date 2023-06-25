using Moq;
using SOAProject.DTOs;
using SOAProject.Models;
using SOAProject.Repositories.AssetEmployeeRepository;

namespace SOAProject.Services.AssetEmployeeService.Tests
{
    public class AssetEmployeeServiceTests
    {
        private readonly Mock<IAssetEmployeeRepository> _mockAssetEmployeeRepository;
        private readonly AssetEmployeeService _assetEmployeeService;

        public AssetEmployeeServiceTests()
        {
            _mockAssetEmployeeRepository = new Mock<IAssetEmployeeRepository>();
            _assetEmployeeService = new AssetEmployeeService(_mockAssetEmployeeRepository.Object);
        }

        [Fact]
        public async Task GetAllAssetEmployees_ShouldReturnAllAssetEmployees()
        {
            // Arrange
            var assetEmployees = new List<Asset_EmployeeResponseDTO>
            {
                new Asset_EmployeeResponseDTO { Id = 1, AssetId = 1, EmployeeId = 1 },
                new Asset_EmployeeResponseDTO { Id = 2, AssetId = 2, EmployeeId = 2 }
            };

            _mockAssetEmployeeRepository.Setup(repo => repo.GetAllAssetsEmployee())
                .ReturnsAsync(assetEmployees);

            // Act
            var result = await _assetEmployeeService.GetAllAssetEmployees();

            // Assert
            Assert.Equal(assetEmployees.Count, result.Count());
            Assert.Equal(assetEmployees, result);
            _mockAssetEmployeeRepository.Verify(repo => repo.GetAllAssetsEmployee(), Times.Once);
        }

        [Fact]
        public async Task GetAssetByEmployeeId_ExistingId_ShouldReturnAssetEmployee()
        {
            // Arrange
            var employeeId = 1;
            var assetEmployee = new Asset_EmployeeResponseDTO { Id = 1, AssetId = 1, EmployeeId = 1 };

            _mockAssetEmployeeRepository.Setup(repo => repo.GetAssetEmployeeByEmployeeId(employeeId))
                .ReturnsAsync(assetEmployee);

            // Act
            var result = await _assetEmployeeService.GetAssetByEmployeeId(employeeId);

            // Assert
            Assert.Equal(assetEmployee, result);
            _mockAssetEmployeeRepository.Verify(repo => repo.GetAssetEmployeeByEmployeeId(employeeId), Times.Once);
        }

        [Fact]
        public async Task GetAssetByEmployeeId_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            var employeeId = 1;

            _mockAssetEmployeeRepository.Setup(repo => repo.GetAssetEmployeeByEmployeeId(employeeId))
                .ReturnsAsync((Asset_EmployeeResponseDTO)null);

            // Act
            var result = await _assetEmployeeService.GetAssetByEmployeeId(employeeId);

            // Assert
            Assert.Null(result);
            _mockAssetEmployeeRepository.Verify(repo => repo.GetAssetEmployeeByEmployeeId(employeeId), Times.Once);
        }

        [Fact]
        public async Task GetAsseEmployeeById_ExistingId_ShouldReturnAssetEmployee()
        {
            // Arrange
            var assetEmployeeId = 1;
            var assetEmployee = new Asset_Employee { Id = assetEmployeeId, AssetId = 1, EmployeeId = 1 };

            _mockAssetEmployeeRepository.Setup(repo => repo.GetAssetEmployeeById(assetEmployeeId))
                .ReturnsAsync(assetEmployee);

            // Act
            var result = await _assetEmployeeService.GetAsseEmployeeById(assetEmployeeId);

            // Assert
            Assert.Equal(assetEmployee, result);
            _mockAssetEmployeeRepository.Verify(repo => repo.GetAssetEmployeeById(assetEmployeeId), Times.Once);
        }

        [Fact]
        public async Task GetAsseEmployeeById_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            var assetEmployeeId = 1;

            _mockAssetEmployeeRepository.Setup(repo => repo.GetAssetEmployeeById(assetEmployeeId))
                .ReturnsAsync((Asset_Employee)null);

            // Act
            var result = await _assetEmployeeService.GetAsseEmployeeById(assetEmployeeId);

            // Assert
            Assert.Null(result);
            _mockAssetEmployeeRepository.Verify(repo => repo.GetAssetEmployeeById(assetEmployeeId), Times.Once);
        }

        [Fact]
        public async Task CreateAssetEmployee_ValidItem_ShouldReturnCreatedAssetEmployee()
        {
            // Arrange
            var item = new Asset_EmployeeDTO { EmployeeId = 1, AssetId = 1 };
            var assetEmployee = new Asset_Employee { Id = 1, EmployeeId = 1, AssetId = 1 };

            _mockAssetEmployeeRepository.Setup(repo => repo.GetAssetEmployeeById(item.AssetId))
                .ReturnsAsync((Asset_Employee)null);
            _mockAssetEmployeeRepository.Setup(repo => repo.AddAssetEmployeeAsync(It.IsAny<Asset_Employee>()));

            // Act
            var result = await _assetEmployeeService.CreateAssetEmployee(item);

            // Assert
            Assert.Equal(assetEmployee.EmployeeId, result.EmployeeId);
            Assert.Equal(assetEmployee.AssetId, result.AssetId);
           }

        [Fact]
        public async Task CreateAssetEmployee_DuplicateItemId_ShouldReturnNull()
        {
            // Arrange
            var item = new Asset_EmployeeDTO { EmployeeId = 1, AssetId = 1 };

            _mockAssetEmployeeRepository.Setup(repo => repo.GetAssetEmployeeById(item.AssetId))
                .ReturnsAsync(new Asset_Employee { Id = 1, EmployeeId = 2, AssetId = 1 });

            // Act
            var result = await _assetEmployeeService.CreateAssetEmployee(item);

            // Assert
            Assert.Null(result);
            _mockAssetEmployeeRepository.Verify(repo => repo.GetAssetEmployeeById(item.AssetId), Times.Once);
            _mockAssetEmployeeRepository.Verify(repo => repo.AddAssetEmployeeAsync(It.IsAny<Asset_Employee>()),
                Times.Never);
        }

        [Fact]
        public async Task DeleteAssetEmployee_ExistingId_ShouldDeleteAssetEmployee()
        {
            // Arrange
            var assetEmployeeId = 1;
            var assetEmployee = new Asset_Employee { Id = assetEmployeeId, EmployeeId = 1, AssetId = 1 };

            _mockAssetEmployeeRepository.Setup(repo => repo.GetAssetEmployeeById(assetEmployeeId))
                .ReturnsAsync(assetEmployee);
            _mockAssetEmployeeRepository.Setup(repo => repo.RemoveAssetEmployee(It.IsAny<Asset_Employee>()));

            // Act
            var result = await _assetEmployeeService.DeleteAssetEmployee(assetEmployeeId);

            // Assert
            Assert.Equal("Successfully delete", result);
         }

        [Fact]
        public async Task DeleteAssetEmployee_NonExistingId_ShouldThrowArgumentException()
        {
            // Arrange
            var assetEmployeeId = 1;

            _mockAssetEmployeeRepository.Setup(repo => repo.GetAssetEmployeeById(assetEmployeeId))
                .ReturnsAsync((Asset_Employee)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                () => _assetEmployeeService.DeleteAssetEmployee(assetEmployeeId));
        }
    }
}