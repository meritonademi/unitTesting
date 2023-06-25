using Microsoft.AspNetCore.Mvc;
using Moq;
using SOAProject.Controllers;
using SOAProject.Models;
using SOAProject.Services.DepartmentService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SOAProject.Tests.Controllers
{
    public class DepartmentControllerTests
    {
        private readonly DepartmentController _departmentController;
        private readonly Mock<IDepartmentService> _mockDepartmentService;

        public DepartmentControllerTests()
        {
            _mockDepartmentService = new Mock<IDepartmentService>();
            _departmentController = new DepartmentController(_mockDepartmentService.Object);
        }

        [Fact]
        public async Task GetAllDepartment_ShouldReturnOkResultWithData()
        {
            var departments = new List<Department>
            {
                new Department { Id = 1, Name = "Department 1" },
                new Department { Id = 2, Name = "Department 2" }
            };

            _mockDepartmentService.Setup(service => service.GetAllDepartment())
                .ReturnsAsync(departments);

            var result = await _departmentController.GetAllDepartment();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var data = Assert.IsType<List<Department>>(okResult.Value);
            Assert.Equal(departments, data);
        }

        [Fact]
        public async Task GetDepartmentById_ExistingId_ShouldReturnOkResultWithData()
        {
            var departmentId = 1;
            var department = new Department { Id = departmentId, Name = "Department 1" };

            _mockDepartmentService.Setup(service => service.GetDepartmentById(departmentId))
                .ReturnsAsync(department);

            var result = await _departmentController.GetDepartmentById(departmentId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var data = Assert.IsType<Department>(okResult.Value);
            Assert.Equal(department, data);
        }

        [Fact]
        public async Task GetDepartmentById_NonExistingId_ShouldReturnNotFound()
        {
            var departmentId = 1;

            _mockDepartmentService.Setup(service => service.GetDepartmentById(departmentId))
                .ReturnsAsync((Department)null);

            var result = await _departmentController.GetDepartmentById(departmentId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateDepartment_ValidDepartment_ShouldReturnOkResultWithData()
        {
            var department = new Department { Id = 1, Name = "Department 1" };

            _mockDepartmentService.Setup(service => service.CreateDepartment(department));

            var result = await _departmentController.CreateDepartment(department);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var data = Assert.IsType<Department>(okResult.Value);
            Assert.Equal(department, data);
        }

        [Fact]
        public async Task UpdateItemAsync_ExistingIdAndMatchingDepartment_ShouldReturnOkResultWithMessage()
        {
            var departmentId = 1;
            var department = new Department { Id = departmentId, Name = "Department 1" };

            _mockDepartmentService.Setup(service => service.UpdateDepartmentAsync(departmentId, department))
                .ReturnsAsync("Department updated");

            var result = await _departmentController.UpdateItemAsync(departmentId, department);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var message = Assert.IsType<string>(okResult.Value);
            Assert.Equal("Department updated", message);
        }

 

        [Fact]
        public async Task UpdateItemAsync_NonExistingId_ShouldReturnNotFound()
        {
            var departmentId = 1;
            var department = new Department { Id = departmentId, Name = "Department 1" };

            _mockDepartmentService.Setup(service => service.UpdateDepartmentAsync(departmentId, department))
                .ThrowsAsync(new InvalidOperationException());

            var result = await _departmentController.UpdateItemAsync(departmentId, department);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteDepartment_ExistingId_ShouldReturnOkResultWithMessage()
        {
            var departmentId = 1;
            var department = new Department { Id = departmentId, Name = "Department 1" };

            _mockDepartmentService.Setup(service => service.GetDepartmentById(departmentId))
                .ReturnsAsync(department);

            _mockDepartmentService.Setup(service => service.DeleteDepartment(departmentId))
                .ReturnsAsync("Department deleted");

            var result = await _departmentController.DeleteDepartment(departmentId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var message = Assert.IsType<string>(okResult.Value);
            Assert.Equal("Department deleted", message);
        }

        [Fact]
        public async Task DeleteDepartment_NonExistingId_ShouldReturnNotFound()
        {
            var departmentId = 1;

            _mockDepartmentService.Setup(service => service.GetDepartmentById(departmentId))
                .ReturnsAsync((Department)null);

            var result = await _departmentController.DeleteDepartment(departmentId);

            Assert.IsType<NotFoundResult>(result);
           }
    }
}
