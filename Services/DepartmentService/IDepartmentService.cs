using ItemManagementSystem1.Models;

namespace ItemManagementSystem1.Services.DepartmentService;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetAllDepartment();
    Task<Department> GetDepartmentById(int id);

    Task<Department> CreateDepartment(Department item);
    Task<string> UpdateDepartmentAsync(int id, Department department);

    Task<string> DeleteDepartment(int id);
}