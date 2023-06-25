using Microsoft.EntityFrameworkCore;
using SOAProject.Data;
using SOAProject.Models;
using SOAProject.Repositories.AssetEmployeeRepository;

namespace SOAProject.Tests
{
    public class AssetEmployeeRepositoryTests : IDisposable
    {
        private readonly DbContextOptions<AppDbContext> _options;
        private readonly AppDbContext _dbContext;
        private readonly IAssetEmployeeRepository _assetEmployeeRepository;

        public AssetEmployeeRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).EnableSensitiveDataLogging().Options;

            _dbContext = new AppDbContext(_options);
            _assetEmployeeRepository = new AssetEmployeeRepository(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Fact]
        public async Task GetAllAssetsEmployee_ShouldReturnAllAssetsEmployee()
        {
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Employee 1", SurName = "Surname 1", Tel = "test" },
                new Employee { Id = 2, Name = "Employee 2", SurName = "Surname 2", Tel = "test" }
            };

            var assets = new List<Asset>
            {
                new Asset { Id = 1, Name = "Asset 1", SerialNr = "asset1" },
                new Asset { Id = 2, Name = "Asset 2", SerialNr = "asset2" },
                new Asset { Id = 3, Name = "Asset 3", SerialNr = "asset3" }
            };

            var assetsEmployees = new List<Asset_Employee>
            {
                new Asset_Employee { EmployeeId = 1, AssetId = 1 },
                new Asset_Employee { EmployeeId = 1, AssetId = 2 },
                new Asset_Employee { EmployeeId = 2, AssetId = 3 }
            };

            _dbContext.Employees.AddRange(employees);
            _dbContext.Assets.AddRange(assets);
            _dbContext.AssetsEmployees.AddRange(assetsEmployees);
            _dbContext.SaveChanges();

            var result = await _assetEmployeeRepository.GetAllAssetsEmployee();

            Assert.Equal(employees.Count, result.Count());
            Assert.Equal(assets.Count, result.Sum(ae => ae.Asset.Count));
        }

        [Fact]
        public async Task GetAssetEmployeeByEmployeeId_ExistingId_ShouldReturnAssetEmployee()
        {
            var employeeId = 1;

            var employee = new Employee { Id = employeeId, Name = "Employee 1", SurName = "Surname 1", Tel = "test" };
            var asset1 = new Asset { Id = 1, Name = "Asset 1", SerialNr = "asset1" };
            var asset2 = new Asset { Id = 2, Name = "Asset 2", SerialNr = "asset2" };

            var assetsEmployees = new List<Asset_Employee>
            {
                new Asset_Employee { EmployeeId = employeeId, AssetId = 1 },
                new Asset_Employee { EmployeeId = employeeId, AssetId = 2 }
            };

            _dbContext.Employees.Add(employee);
            _dbContext.Assets.AddRange(asset1, asset2);
            _dbContext.AssetsEmployees.AddRange(assetsEmployees);
            _dbContext.SaveChanges();

            var result = await _assetEmployeeRepository.GetAssetEmployeeByEmployeeId(employeeId);

            Assert.NotNull(result);
            Assert.Equal(employeeId, result.EmployeeId);
            Assert.Equal(2, result.Asset.Count);
        }

        [Fact]
        public async Task GetAssetEmployeeByEmployeeId_NonExistingId_ShouldReturnNull()
        {
            var result = await _assetEmployeeRepository.GetAssetEmployeeByEmployeeId(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAssetEmployeeById_ExistingId_ShouldReturnAssetEmployee()
        {
            var assetEmployee = new Asset_Employee { EmployeeId = 1, AssetId = 1 };
            _dbContext.AssetsEmployees.Add(assetEmployee);
            _dbContext.SaveChanges();

            var result = await _assetEmployeeRepository.GetAssetEmployeeById(assetEmployee.AssetId);

            Assert.NotNull(result);
            Assert.Equal(assetEmployee.AssetId, result.AssetId);
            Assert.Equal(assetEmployee.EmployeeId, result.EmployeeId);
        }

        [Fact]
        public async Task GetAssetEmployeeById_NonExistingId_ShouldReturnNull()
        {
            var result = await _assetEmployeeRepository.GetAssetEmployeeById(1);
            Assert.Null(result);
        }

        [Fact]
        public void AddAssetEmployeeAsync_ShouldAddAssetEmployeeToDatabase()
        {
            var assetEmployee = new Asset_Employee { EmployeeId = 1, AssetId = 1 };

            var result = _assetEmployeeRepository.AddAssetEmployeeAsync(assetEmployee);

            Assert.NotNull(result);
            Assert.Equal(assetEmployee.EmployeeId, result.EmployeeId);
            Assert.Equal(assetEmployee.AssetId, result.AssetId);
        }
        

        [Fact]
        public void RemoveAssetEmployee_ShouldRemoveAssetEmployeeFromDatabase()
        {
            var assetEmployee = new Asset_Employee { EmployeeId = 1, AssetId = 1 };
            _dbContext.AssetsEmployees.Add(assetEmployee);
            _dbContext.SaveChanges();

            _assetEmployeeRepository.RemoveAssetEmployee(assetEmployee);

            var result = _dbContext.AssetsEmployees.Find(assetEmployee.AssetId);
            Assert.Null(result);
        }
    }
}