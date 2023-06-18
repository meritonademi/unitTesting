using ItemManagementSystem1.Models;

namespace ItemManagementSystem1.Services.EmployeeService;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployee();
    Task<Employee> GetEmployeeById(int id);

    Task<Employee> CreateEmployee(Employee item);

    Task<string> UpdateEmployeeAsync(int id, Employee employee);

    Task<string> DeleteEmployee(int id);
}