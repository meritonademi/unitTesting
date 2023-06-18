using ItemManagementSystem1.Models;
using ItemManagementSystem1.Repositories.EmployeeRepository;

namespace ItemManagementSystem1.Services.EmployeeService;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployee()
    {
        var items = await _employeeRepository.GetAllEmployees();
        return items;
    }

    public async Task<Employee> GetEmployeeById(int id)
    {
        var item = await _employeeRepository.GetEmployeeById(id);
        return item;
    }

    public async Task<Employee> CreateEmployee(Employee item)
    {
        try
        {
            _employeeRepository.AddEmployeeAsync(item);
            return item;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> UpdateEmployeeAsync(int id, Employee employee)
    {
        try
        {
            var item = await _employeeRepository.GetEmployeeById(id);
            if (item == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            _employeeRepository.UpdateEmployee(item, employee);
            return "Successfully update";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> DeleteEmployee(int id)
    {
        var item = await _employeeRepository.GetEmployeeById(id);
        if (item == null)
        {
            throw new ArgumentException("Employee not found.");
        }

        _employeeRepository.RemoveEmployee(item);
        return "Successfully delete";
    }
}