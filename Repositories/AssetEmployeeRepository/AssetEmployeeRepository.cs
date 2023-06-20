using SOAProject.Data;
using SOAProject.DTOs;
using SOAProject.Models;

namespace SOAProject.Repositories.AssetEmployeeRepository;

public class AssetEmployeeRepository : IAssetEmployeeRepository
{
    private readonly AppDbContext _dbContext;

    public AssetEmployeeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<Asset_EmployeeResponseDTO>> GetAllAssetsEmployee()
    {
        Dictionary<int, Asset_EmployeeResponseDTO> assetsEmployees =
            new Dictionary<int, Asset_EmployeeResponseDTO>();

        var fullEntries = _dbContext.AssetsEmployees.Join(
            _dbContext.Employees,
            assetsEmployees => assetsEmployees.EmployeeId,
            employees => employees.Id,
            (assetsEmployees, employees) => new { assetsEmployees, employees }
        ).Join(
            _dbContext.Assets,
            assetsEmployees => assetsEmployees.assetsEmployees.AssetId,
            assets => assets.Id,
            (assetsEmployees, assets) => new { assetsEmployees.assetsEmployees, assetsEmployees.employees, assets }
        );


        foreach (var fullEntry in fullEntries)
        {
            if (assetsEmployees.GetValueOrDefault(fullEntry.assetsEmployees.EmployeeId) == null)
            {
                Asset_EmployeeResponseDTO assetEmployeeDto = new Asset_EmployeeResponseDTO();
                assetEmployeeDto.EmployeeId = fullEntry.assetsEmployees.EmployeeId;
                assetEmployeeDto.AssetId = fullEntry.assetsEmployees.AssetId;
                assetEmployeeDto.Employee = new Employee();
                assetEmployeeDto.Asset = new List<Asset>();
                assetsEmployees.Add(fullEntry.assetsEmployees.EmployeeId,
                    assetEmployeeDto);
                assetsEmployees.GetValueOrDefault(fullEntry.assetsEmployees.EmployeeId)!.Employee =
                    fullEntry.employees;
            }

            assetsEmployees.GetValueOrDefault(fullEntry.assetsEmployees.EmployeeId)!.Asset.Add(
                fullEntry.assets);
        }

        return assetsEmployees.Values;
    }

    public async Task<Asset_EmployeeResponseDTO> GetAssetEmployeeByEmployeeId(int id)
    {
        var fullEntries = _dbContext.AssetsEmployees.Join(
            _dbContext.Employees,
            assetsEmployees => assetsEmployees.EmployeeId,
            employees => employees.Id,
            (assetsEmployees, employees) => new { assetsEmployees, employees }
        ).Join(
            _dbContext.Assets,
            assetsEmployees => assetsEmployees.assetsEmployees.AssetId,
            assets => assets.Id,
            (assetsEmployees, assets) => new { assetsEmployees, assets }
        ).Where(fullyEntries => fullyEntries.assetsEmployees.assetsEmployees.EmployeeId.Equals(id));



        Asset_EmployeeResponseDTO assetEmployee = new Asset_EmployeeResponseDTO();
        foreach (var fullEntry in fullEntries)
        {
            if (assetEmployee.EmployeeId == 0)
            {
                assetEmployee.EmployeeId = fullEntry.assetsEmployees.assetsEmployees.EmployeeId;
                assetEmployee.AssetId = fullEntry.assetsEmployees.assetsEmployees.AssetId;
                assetEmployee.Employee = fullEntry.assetsEmployees.employees;
                assetEmployee.Asset = new List<Asset>();
            }

            assetEmployee.Asset.Add(fullEntry.assets);
        }

        if (assetEmployee.EmployeeId == 0)
        {
            return null;
        }

        return assetEmployee;
    }

    public async Task<Asset_Employee> GetAssetEmployeeById(int id)
    {
        var fullEntries = _dbContext.AssetsEmployees.Where(fullyEntries => fullyEntries.AssetId.Equals(id));
        Asset_Employee assetEmployee = null;
        foreach (var fullEntry in fullEntries)
        {
            assetEmployee = new Asset_Employee();
            assetEmployee.EmployeeId = fullEntry.EmployeeId;
            assetEmployee.AssetId = fullEntry.AssetId;
        }

        return assetEmployee;
    }

    public Asset_Employee AddAssetEmployeeAsync(Asset_Employee assetEmployee)
    {
        _dbContext.AssetsEmployees.Add(assetEmployee);
        _dbContext.SaveChanges();
        return assetEmployee;
    }

    public void UpdateAsset(Asset_Employee assetEmployee, Asset_Employee assetEmployeeToUpdate)
    {
        assetEmployee.EmployeeId = assetEmployeeToUpdate.EmployeeId;
        assetEmployee.AssetId = assetEmployeeToUpdate.AssetId;
        _dbContext.AssetsEmployees.Update(assetEmployee);
        _dbContext.SaveChanges();
    }

    public void RemoveAssetEmployee(Asset_Employee assetEmployee)
    {
        _dbContext.AssetsEmployees.Remove(assetEmployee);
        _dbContext.SaveChanges();
    }
}