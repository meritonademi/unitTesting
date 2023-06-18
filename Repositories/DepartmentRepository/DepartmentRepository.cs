using ItemManagementSystem1.Data;
using ItemManagementSystem1.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemManagementSystem1.Repositories.DepartmentRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext;

        public DepartmentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Department>> GetAllDepartment()
        {
            return await _dbContext.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await _dbContext.Departments.FindAsync(id);
        }

        public async Task AddDepartmentAsync(Department department)
        {
            await _dbContext.Departments.AddAsync(department);
            _dbContext.SaveChanges();
        }

        public void UpdateDepartment(Department department, Department departmentToUpdate)
        {
            department.Id = departmentToUpdate.Id;
            department.Name = departmentToUpdate.Name;
            _dbContext.Departments.Update(department);
            _dbContext.SaveChanges();
        }

        public void RemoveDepartment(Department department)
        {
            _dbContext.Departments.Remove(department);
            _dbContext.SaveChanges();
        }
    }
}