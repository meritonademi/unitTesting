using ItemManagementSystem1.Models;
using ItemManagementSystem1.Repositories.DepartmentRepository;

namespace ItemManagementSystem1.Services.DepartmentService;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<IEnumerable<Department>> GetAllDepartment()
    {
        var items = await _departmentRepository.GetAllDepartment();
        return items;
    }

    public async Task<Department> GetDepartmentById(int id)
    {
        var item = await _departmentRepository.GetDepartmentById(id);
        return item;
    }

    public async Task<Department> CreateDepartment(Department item)
    {
        try
        {
            _departmentRepository.AddDepartmentAsync(item);
            return item;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> UpdateDepartmentAsync(int id, Department department)
    {
        try
        {
            var item = await _departmentRepository.GetDepartmentById(id);
            if (item == null)
            {
                throw new ArgumentException("Department not found.");
            }

            _departmentRepository.UpdateDepartment(item, department);
            return "Successfully update";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> DeleteDepartment(int id)
    {
        var item = await _departmentRepository.GetDepartmentById(id);
        if (item == null)
        {
            throw new ArgumentException("Department not found.");
        }

        _departmentRepository.RemoveDepartment(item);
        return "Successfully delete";
    }
}