using SOAProject.Data;
using SOAProject.DTOs;
using SOAProject.Models;

namespace SOAProject.Repositories.EmployeeRepository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _dbContext;

    public EmployeeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        List<Employee> employee = new List<Employee>(_dbContext.Employees);

        var fullEntries = _dbContext.Employees.Join(
            _dbContext.Departments,
            employee => employee.DepartmentId,
            department => department.Id,
            (employee, department) => new { employee, department }
        );

        foreach (var fullEntry in fullEntries)
        {
            Employee item = new Employee();
            item = fullEntry.employee;
            item.Department = fullEntry.department;
            employee.Add(item);
        }

        return employee;
    }

    public async Task<Employee> GetEmployeeById(int id)
    {
        var fullEntries = _dbContext.Employees.Join(
            _dbContext.Departments,
            employee => employee.DepartmentId,
            department => department.Id,
            (employee, department) => new { employee, department }
        ).Where(fullyEntries => fullyEntries.employee.Id.Equals(id));


        Employee employee = new Employee();
        foreach (var fullEntry in fullEntries)
        {
            employee = fullEntry.employee;
            employee.Department = fullEntry.department;
        }

        if (employee.Id == 0)
        {
            return null;
        }

        return employee;
    }

    public Employee AddEmployeeAsync(Employee employee)
    {
        _dbContext.Employees.Add(employee);
        _dbContext.SaveChanges();
        return employee;
    }

    public void UpdateEmployee(Employee employee, EmployeeDTO employeeToUpdate)
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