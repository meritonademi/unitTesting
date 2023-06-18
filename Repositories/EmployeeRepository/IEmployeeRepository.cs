using ItemManagementSystem1.Models;

namespace ItemManagementSystem1.Repositories.EmployeeRepository;

public interface IEmployeeRepository
{
    void RemoveEmployee(Employee employee);
    void UpdateEmployee(Employee employee, Employee employeeToUpdate);
    Employee AddEmployeeAsync(Employee employee);
    Task<Employee> GetEmployeeById(int id);
    Task<IEnumerable<Employee>> GetAllEmployees();
}