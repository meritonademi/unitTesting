using Microsoft.EntityFrameworkCore;
using SOAProject.Data;
using SOAProject.Models;
using SOAProject.Repositories.DepartmentRepository;

namespace SOAProject.Tests
{
    public class DepartmentRepositoryTests : IDisposable
    {
        private readonly DbContextOptions<AppDbContext> _options;
        private readonly AppDbContext _dbContext;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).EnableSensitiveDataLogging().Options;
            _dbContext = new AppDbContext(_options);
            _departmentRepository = new DepartmentRepository(_dbContext);
        }

        public void Dispose()
        {
            // Clean up the database after each test
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Fact]
        public async Task GetAllDepartment_ShouldReturnAllDepartments()
        {
            // Arrange
            var departments = new List<Department>
            {
                new Department { Id = 1, Name = "Department 1" },
                new Department { Id = 2, Name = "Department 2" },
                new Department { Id = 3, Name = "Department 3" }
            };
            _dbContext.Departments.AddRange(departments);
            _dbContext.SaveChanges();

     
            var result = await _departmentRepository.GetAllDepartment();

            Assert.Equal(departments.Count(), result.Count());
        }

        [Fact]
        public async Task GetDepartmentById_ExistingId_ShouldReturnDepartment()
        {
            // Arrange
            var department = new Department { Id = 1, Name = "Department 1" };
            _dbContext.Departments.Add(department);
            _dbContext.SaveChanges();
            var result = await _departmentRepository.GetDepartmentById(department.Id);
            Assert.NotNull(result);
            Assert.Equal(department.Id, result.Id);
        }

        [Fact]
        public async Task GetDepartmentById_NonExistingId_ShouldReturnNull()
        {
            var result = await _departmentRepository.GetDepartmentById(1);
            Assert.Null(result);
        }

        [Fact]
        public async Task AddDepartmentAsync_ShouldAddDepartmentToDatabase()
        {
            var department = new Department { Id = 1, Name = "Department 1" };

            await _departmentRepository.AddDepartmentAsync(department);

            var result = await _dbContext.Departments.FindAsync(department.Id);
            Assert.NotNull(result);
            Assert.Equal(department.Id, result.Id);
            Assert.Equal(department.Name, result.Name);
        }

        [Fact]
        public async Task UpdateDepartment_ShouldUpdateDepartmentInDatabase()
        {
            var existingDepartment = new Department { Id = 1, Name = "Department 1" };
            _dbContext.Departments.Add(existingDepartment);
            _dbContext.SaveChanges();

            var updatedDepartment = new Department { Id = 1, Name = "Updated Department 1" };
            _departmentRepository.UpdateDepartment(existingDepartment, updatedDepartment);

            var result = await _dbContext.Departments.FindAsync(existingDepartment.Id);
            Assert.NotNull(result);
            Assert.Equal(updatedDepartment.Name, result.Name);
        }

        [Fact]
        public async Task RemoveDepartment_ShouldRemoveDepartmentFromDatabase()
        {
            var department = new Department { Id = 1, Name = "Department 1" };
            _dbContext.Departments.Add(department);
            _dbContext.SaveChanges();

            _departmentRepository.RemoveDepartment(department);

            var result = await _dbContext.Departments.FindAsync(department.Id);
            Assert.Null(result);
        }
    }
}