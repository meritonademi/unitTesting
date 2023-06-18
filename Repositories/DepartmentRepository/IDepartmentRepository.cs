using ItemManagementSystem1.Models;

namespace ItemManagementSystem1.Repositories.DepartmentRepository;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllDepartment();
    Task<Department> GetDepartmentById(int id);

    Task AddDepartmentAsync(Department department);
    void UpdateDepartment(Department department);
    void RemoveDepartment(Department department);
}