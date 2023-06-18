using ItemManagementSystem1.DTOs;
using ItemManagementSystem1.Models;

namespace ItemManagementSystem1.Services.EmployeeService;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployee();
    Task<Employee> GetEmployeeById(int id);

    Task<Employee> CreateEmployee(Employee item);

    Task<string> UpdateEmployeeAsync(int id, EmployeeDTO employee);

    Task<string> DeleteEmployee(int id);
}