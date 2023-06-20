using SOAProject.DTOs;
using SOAProject.Models;

namespace SOAProject.Repositories.EmployeeRepository;

public interface IEmployeeRepository
{
    void RemoveEmployee(Employee employee);
    void UpdateEmployee(Employee employee, EmployeeDTO employeeToUpdate);
    Employee AddEmployeeAsync(Employee employee);
    Task<Employee> GetEmployeeById(int id);
    Task<IEnumerable<Employee>> GetAllEmployees();
}