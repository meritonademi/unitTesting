using Microsoft.AspNetCore.Mvc;
using Moq;
using SOAProject.Controllers;
using SOAProject.DTOs;
using SOAProject.Models;
using SOAProject.Services.EmployeeService;

public class EmployeeControllerTests
{
    private readonly Mock<IEmployeeService> _mockEmployeeService;
    private readonly EmployeeController _employeeController;

    public EmployeeControllerTests()
    {
        _mockEmployeeService = new Mock<IEmployeeService>();
        _employeeController = new EmployeeController(_mockEmployeeService.Object);
    }

    [Fact]
    public async Task GetAllEmployee_ShouldReturnOkResultWithItems()
    {
        var employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John", SurName = "Doe", Tel = "123456789", DepartmentId = 1 },
            new Employee { Id = 2, Name = "Jane", SurName = "Smith", Tel = "987654321", DepartmentId = 2 }
        };

        _mockEmployeeService.Setup(service => service.GetAllEmployee())
            .ReturnsAsync(employees);

        var result = await _employeeController.GetAllEmployee();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedItems = Assert.IsAssignableFrom<IEnumerable<Employee>>(okResult.Value);
        Assert.Equal(employees, returnedItems);
        _mockEmployeeService.Verify(service => service.GetAllEmployee(), Times.Once);
    }

    [Fact]
    public async Task GetEmployeeById_ExistingId_ShouldReturnOkResultWithItem()
    {
        var employeeId = 1;
        var employee = new Employee { Id = employeeId, Name = "John", SurName = "Doe", Tel = "123456789", DepartmentId = 1 };

        _mockEmployeeService.Setup(service => service.GetEmployeeById(employeeId))
            .ReturnsAsync(employee);

        var result = await _employeeController.GetEmployeeById(employeeId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedItem = Assert.IsType<Employee>(okResult.Value);
        Assert.Equal(employee, returnedItem);
    }

    [Fact]
    public async Task GetEmployeeById_NonExistingId_ShouldReturnNotFound()
    {
        var employeeId = 1;

        _mockEmployeeService.Setup(service => service.GetEmployeeById(employeeId))
            .ReturnsAsync((Employee)null);

        var result = await _employeeController.GetEmployeeById(employeeId);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateEmployee_ValidItem_ShouldReturnOkResultWithCreatedItem()
    {
        var item = new EmployeeDTO
        {
            Name = "John",
            SurName = "Doe",
            Tel = "123456789",
            DepartmentId = 1
        };

        var result = await _employeeController.CreateEmployee(item);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedItem = Assert.IsType<EmployeeDTO>(okResult.Value);
        Assert.Equal(item, returnedItem);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_ExistingIdAndMatchingEmployee_ShouldReturnOkResultWithMessage()
    {
        var employeeId = 1;
        var employee = new EmployeeDTO
        {
            Id = employeeId,
            Name = "John",
            SurName = "Doe",
            Tel = "123456789",
            DepartmentId = 1
        };

        _mockEmployeeService.Setup(service => service.UpdateEmployeeAsync(employeeId, employee))
            .ReturnsAsync("Employee updated");

        var result = await _employeeController.UpdateEmployeeAsync(employeeId, employee);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var message = Assert.IsType<string>(okResult.Value);
        Assert.Equal("Employee updated", message);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_ExistingIdAndNonMatchingEmployee_ShouldReturnBadRequest()
    {
        var employeeId = 1;
        var employee = new EmployeeDTO
        {
            Id = 2,
            Name = "John",
            SurName = "Doe",
            Tel = "123456789",
            DepartmentId = 1
        };

        var result = await _employeeController.UpdateEmployeeAsync(employeeId, employee);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_NonExistingId_ShouldReturnNotFound()
    {
        var employeeId = 1;
        var employee = new EmployeeDTO
        {
            Id = employeeId,
            Name = "John",
            SurName = "Doe",
            Tel = "123456789",
            DepartmentId = 1
        };

        _mockEmployeeService.Setup(service => service.UpdateEmployeeAsync(employeeId, employee))
            .ThrowsAsync(new InvalidOperationException());

        var result = await _employeeController.UpdateEmployeeAsync(employeeId, employee);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteEmployee_ExistingId_ShouldReturnOkResultWithMessage()
    {
        var employeeId = 1;
        var employee = new Employee { Id = employeeId, Name = "John", SurName = "Doe", Tel = "123456789", DepartmentId = 1 };

        _mockEmployeeService.Setup(service => service.GetEmployeeById(employeeId))
            .ReturnsAsync(employee);
        _mockEmployeeService.Setup(service => service.DeleteEmployee(employee.Id))
            .ReturnsAsync("Employee deleted");

        var result = await _employeeController.DeleteEmployee(employeeId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var message = Assert.IsType<string>(okResult.Value);
        Assert.Equal("Employee deleted", message);
     }

    [Fact]
    public async Task DeleteEmployee_NonExistingId_ShouldReturnNotFound()
    {
        var employeeId = 1;

        _mockEmployeeService.Setup(service => service.GetEmployeeById(employeeId))
            .ReturnsAsync((Employee)null);

        var result = await _employeeController.DeleteEmployee(employeeId);

        Assert.IsType<NotFoundResult>(result);
        }
}
