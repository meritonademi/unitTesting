using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOAProject.Models;
using SOAProject.Repositories.DepartmentRepository;
using Xunit;

namespace SOAProject.Services.DepartmentService.Tests
{
    public class DepartmentServiceTests
    {
        private readonly Mock<IDepartmentRepository> _mockDepartmentRepository;
        private readonly DepartmentService _departmentService;

        public DepartmentServiceTests()
        {
            _mockDepartmentRepository = new Mock<IDepartmentRepository>();
            _departmentService = new DepartmentService(_mockDepartmentRepository.Object);
        }

        [Fact]
        public async Task GetAllDepartment_ShouldReturnAllDepartments()
        {
            // Arrange
            var departments = new List<Department>
            {
                new Department { Id = 1, Name = "Department 1" },
                new Department { Id = 2, Name = "Department 2" }
            };

            _mockDepartmentRepository.Setup(repo => repo.GetAllDepartment())
                .ReturnsAsync(departments);

            // Act
            var result = await _departmentService.GetAllDepartment();

            // Assert
            Assert.Equal(departments.Count, result.Count());
            Assert.Equal(departments, result);
            _mockDepartmentRepository.Verify(repo => repo.GetAllDepartment(), Times.Once);
        }

        [Fact]
        public async Task GetDepartmentById_ExistingId_ShouldReturnDepartment()
        {
            // Arrange
            var departmentId = 1;
            var department = new Department { Id = departmentId, Name = "Department 1" };

            _mockDepartmentRepository.Setup(repo => repo.GetDepartmentById(departmentId))
                .ReturnsAsync(department);

            // Act
            var result = await _departmentService.GetDepartmentById(departmentId);

            // Assert
            Assert.Equal(department, result);
            _mockDepartmentRepository.Verify(repo => repo.GetDepartmentById(departmentId), Times.Once);
        }

        [Fact]
        public async Task GetDepartmentById_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            var departmentId = 1;

            _mockDepartmentRepository.Setup(repo => repo.GetDepartmentById(departmentId))
                .ReturnsAsync((Department)null);

            // Act
            var result = await _departmentService.GetDepartmentById(departmentId);

            // Assert
            Assert.Null(result);
            _mockDepartmentRepository.Verify(repo => repo.GetDepartmentById(departmentId), Times.Once);
        }

        [Fact]
        public async Task CreateDepartment_ShouldCreateNewDepartment()
        {
            // Arrange
            var department = new Department { Name = "New Department" };

            _mockDepartmentRepository.Setup(repo => repo.AddDepartmentAsync(It.IsAny<Department>()));

            // Act
            var result = await _departmentService.CreateDepartment(department);

            // Assert
            Assert.NotNull(result);
            _mockDepartmentRepository.Verify(repo => repo.AddDepartmentAsync(department), Times.Once);
        }

        [Fact]
        public async Task UpdateDepartmentAsync_ExistingId_ShouldUpdateDepartment()
        {
            // Arrange
            var departmentId = 1;
            var existingDepartment = new Department { Id = departmentId, Name = "Department 1" };
            var updatedDepartment = new Department { Id = departmentId, Name = "Updated Department" };

            _mockDepartmentRepository.Setup(repo => repo.GetDepartmentById(departmentId))
                .ReturnsAsync(existingDepartment);
            _mockDepartmentRepository.Setup(repo => repo.UpdateDepartment(It.IsAny<Department>(), It.IsAny<Department>()));

            // Act
            var result = await _departmentService.UpdateDepartmentAsync(departmentId, updatedDepartment);

            // Assert
            Assert.Equal("Successfully update", result);
            _mockDepartmentRepository.Verify(repo => repo.UpdateDepartment(existingDepartment, updatedDepartment), Times.Once);
        }

        [Fact]
        public async Task UpdateDepartmentAsync_NonExistingId_ShouldThrowArgumentException()
        {
            // Arrange
            var departmentId = 1;
            var updatedDepartment = new Department { Id = departmentId, Name = "Updated Department" };

            _mockDepartmentRepository.Setup(repo => repo.GetDepartmentById(departmentId))
                .ReturnsAsync((Department)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _departmentService.UpdateDepartmentAsync(departmentId, updatedDepartment));
        }

        [Fact]
        public async Task DeleteDepartment_ExistingId_ShouldDeleteDepartment()
        {
            // Arrange
            var departmentId = 1;
            var existingDepartment = new Department { Id = departmentId, Name = "Department 1" };

            _mockDepartmentRepository.Setup(repo => repo.GetDepartmentById(departmentId))
                .ReturnsAsync(existingDepartment);
            _mockDepartmentRepository.Setup(repo => repo.RemoveDepartment(It.IsAny<Department>()));

            // Act
            var result = await _departmentService.DeleteDepartment(departmentId);

            // Assert
            Assert.Equal("Successfully delete", result);
            _mockDepartmentRepository.Verify(repo => repo.RemoveDepartment(existingDepartment), Times.Once);
        }

        [Fact]
        public async Task DeleteDepartment_NonExistingId_ShouldThrowArgumentException()
        {
            // Arrange
            var departmentId = 1;

            _mockDepartmentRepository.Setup(repo => repo.GetDepartmentById(departmentId))
                .ReturnsAsync((Department)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _departmentService.DeleteDepartment(departmentId));
        }
    }
}
