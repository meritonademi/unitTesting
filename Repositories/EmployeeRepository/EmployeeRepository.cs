using ItemManagementSystem1.Data;
using ItemManagementSystem1.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemManagementSystem1.Repositories.EmployeeRepository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _dbContext;

    public EmployeeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        return await _dbContext.Employees.ToListAsync();
    }

    public async Task<Employee> GetEmployeeById(int id)
    {
        return await _dbContext.Employees.FindAsync(id);
    }

    public Employee AddEmployeeAsync(Employee employee)
    {
        _dbContext.Employees.Add(employee);
        _dbContext.SaveChanges();
        return employee;
    }

    public void UpdateEmployee(Employee employee, Employee employeeToUpdate)
    {
        employee.Name = employeeToUpdate.Name;
        employee.SurName = employeeToUpdate.SurName;
        employee.DepartmentId = employeeToUpdate.DepartmentId;
        employee.Tel = employeeToUpdate.Tel;
        _dbContext.Employees.Update(employee);
        _dbContext.SaveChanges();
    }

    public void RemoveEmployee(Employee employee)
    {
        _dbContext.Employees.Remove(employee);
        _dbContext.SaveChanges();
    }
}