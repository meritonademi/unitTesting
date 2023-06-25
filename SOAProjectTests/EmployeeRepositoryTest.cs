using Microsoft.EntityFrameworkCore;
using SOAProject.Data;
using SOAProject.DTOs;
using SOAProject.Models;
using SOAProject.Repositories.EmployeeRepository;

namespace SOAProjectTests;

public class EmployeeRepositoryTest
{
    private readonly DbContextOptions<AppDbContext> _options;

    public EmployeeRepositoryTest()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
    }

    [Fact]
    public async Task GetAllEmployees_ShouldReturnAllEmployeesWithDepartments()
    {
        using (var context = new AppDbContext(_options))
        {
            var employeeRepository = new EmployeeRepository(context);
            await SeedDatabase(context);
            var employees = await employeeRepository.GetAllEmployees();

            Assert.NotNull(employees);
            Assert.Equal(3, employees.Count());
            foreach (var employee in employees)
            {
                Assert.NotNull(employee.Department);
            }
        }
    }

    [Fact]
    public async Task GetEmployeeById_ExistingId_ShouldReturnEmployeeWithDepartment()
    {
        using (var context = new AppDbContext(_options))
        {
            var employeeRepository = new EmployeeRepository(context);
            await SeedDatabase(context);
            var employee = await employeeRepository.GetEmployeeById(1);
            Assert.NotNull(employee);
            Assert.Equal(1, employee.Id);
            Assert.NotNull(employee.Department);
        }
    }

    [Fact]
    public async Task GetEmployeeById_NonExistingId_ShouldReturnNull()
    {
        using (var context = new AppDbContext(_options))
        {
            var employeeRepository = new EmployeeRepository(context);
            await SeedDatabase(context);
            var employee = await employeeRepository.GetEmployeeById(99);
            Assert.Null(employee);
        }
    }

    [Fact]
    public void AddEmployeeAsync_ShouldAddEmployeeToDatabase()
    {
        using (var context = new AppDbContext(_options))
        {
            var employeeRepository = new EmployeeRepository(context);
            var employee = new Employee
                { Id = 4, Name = "John", SurName = "Doe", DepartmentId = 1, Tel = "1234567890" };

            var addedEmployee = employeeRepository.AddEmployeeAsync(employee);

            Assert.Equal(4, addedEmployee.Id);
            Assert.Contains(context.Employees, e => e.Id == 4);
        }
    }

    [Fact]
    public async Task UpdateEmployee_ShouldUpdateEmployeeInDatabase()
    {
        using (var context = new AppDbContext(_options))
        {
            var employeeRepository = new EmployeeRepository(context);
            await SeedDatabase(context);
            var employeeToUpdate = new EmployeeDTO
                { Name = "Updated", SurName = "Employee", DepartmentId = 2, Tel = "9876543210" };

            var employee = await employeeRepository.GetEmployeeById(1);
            employeeRepository.UpdateEmployee(employee, employeeToUpdate);

            Assert.Equal("Updated", employee.Name);
            Assert.Equal("Employee", employee.SurName);
            Assert.Equal(2, employee.DepartmentId);
            Assert.Equal("9876543210", employee.Tel);
        }
    }

    [Fact]
    public void RemoveEmployee_ShouldRemoveEmployeeFromDatabase()
    {
        using (var context = new AppDbContext(_options))
        {
            var employeeRepository = new EmployeeRepository(context);
            var employeeToRemove = new Employee
                { Id = 1, Name = "John", SurName = "Doe", DepartmentId = 1, Tel = "1234567890" };
            employeeRepository.RemoveEmployee(employeeToRemove);
            Assert.DoesNotContain(context.Employees, e => e.Id == 1);
        }
    }

    private async Task SeedDatabase(AppDbContext context)
    {
        var department1 = new Department { Id = 1, Name = "Department 1" };
        var department2 = new Department { Id = 2, Name = "Department 2" };
        var employee1 = new Employee { Id = 1, Name = "John", SurName = "Doe", DepartmentId = 1, Tel = "1234567890" };
        var employee2 = new Employee { Id = 2, Name = "Jane", SurName = "Smith", DepartmentId = 2, Tel = "9876543210" };
        var employee3 = new Employee { Id = 3, Name = "Alice", SurName = "Johnson", DepartmentId = 1, Tel = "5555555555" };

        context.Departments.AddRange(department1, department2);
        context.Employees.AddRange(employee1, employee2, employee3);
        await context.SaveChangesAsync();
    }
}