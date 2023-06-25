using Moq;
using SOAProject.DTOs;
using SOAProject.Models;
using SOAProject.Repositories.EmployeeRepository;
using SOAProject.Services.EmployeeService;

namespace SOAProject.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task GetAllEmployee_ShouldReturnAllEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Employee 1" },
                new Employee { Id = 2, Name = "Employee 2" },
                new Employee { Id = 3, Name = "Employee 3" }
            };

            _mockEmployeeRepository.Setup(repo => repo.GetAllEmployees())
                .ReturnsAsync(employees);

            var result = await _employeeService.GetAllEmployee();

            Assert.Equal(employees.Count, result.Count());
        }

        [Fact]
        public async Task GetEmployeeById_ExistingId_ShouldReturnEmployee()
        {
            var employeeId = 1;
            var employee = new Employee { Id = employeeId, Name = "Employee 1" };

            _mockEmployeeRepository.Setup(repo => repo.GetEmployeeById(employeeId))
                .ReturnsAsync(employee);

            var result = await _employeeService.GetEmployeeById(employeeId);

            Assert.NotNull(result);
            Assert.Equal(employeeId, result.Id);
        }

        [Fact]
        public async Task GetEmployeeById_NonExistingId_ShouldReturnNull()
        {
            var employeeId = 1;

            _mockEmployeeRepository.Setup(repo => repo.GetEmployeeById(employeeId))
                .ReturnsAsync((Employee)null);

            var result = await _employeeService.GetEmployeeById(employeeId);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateEmployee_ShouldCreateNewEmployee()
        {
            var employee = new Employee { Name = "New Employee" };

            _mockEmployeeRepository.Setup(repo => repo.AddEmployeeAsync(It.IsAny<Employee>()));

            var result = await _employeeService.CreateEmployee(employee);

            Assert.NotNull(result);
            _mockEmployeeRepository.Verify(repo => repo.AddEmployeeAsync(employee), Times.Once);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ExistingEmployee_ShouldUpdateEmployee()
        {
            var employeeId = 1;
            var employeeDto = new EmployeeDTO { Name = "Updated Employee" };
            var existingEmployee = new Employee { Id = employeeId, Name = "Employee 1" };

            _mockEmployeeRepository.Setup(repo => repo.GetEmployeeById(employeeId))
                .ReturnsAsync(existingEmployee);

            _mockEmployeeRepository.Setup(repo => repo.UpdateEmployee(existingEmployee, employeeDto));

            var result = await _employeeService.UpdateEmployeeAsync(employeeId, employeeDto);

            Assert.Equal("Successfully update", result);
            _mockEmployeeRepository.Verify(repo => repo.UpdateEmployee(existingEmployee, employeeDto), Times.Once);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_NonExistingEmployee_ShouldThrowException()
        {
            var employeeId = 1;
            var employeeDto = new EmployeeDTO { Name = "Updated Employee" };

            _mockEmployeeRepository.Setup(repo => repo.GetEmployeeById(employeeId))
                .ReturnsAsync((Employee)null);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                _employeeService.UpdateEmployeeAsync(employeeId, employeeDto));
        }
    }
}