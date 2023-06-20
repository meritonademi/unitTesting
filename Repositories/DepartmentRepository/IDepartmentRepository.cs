using SOAProject.Models;

namespace SOAProject.Repositories.DepartmentRepository;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllDepartment();
    Task<Department> GetDepartmentById(int id);

    Task AddDepartmentAsync(Department department);
    void UpdateDepartment(Department department, Department departmentToUpdate);
    void RemoveDepartment(Department department);
}