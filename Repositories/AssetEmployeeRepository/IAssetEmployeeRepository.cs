using ItemManagementSystem1.DTOs;
using ItemManagementSystem1.Models;

namespace ItemManagementSystem1.Repositories.AssetEmployeeRepository;

public interface IAssetEmployeeRepository
{
    Task<IEnumerable<Asset_EmployeeResponseDTO>> GetAllAssetsEmployee();
    Task<Asset_EmployeeResponseDTO> GetAssetEmployeeByEmployeeId(int id);

    Asset_Employee AddAssetEmployeeAsync(Asset_Employee assetEmployee);

    void UpdateAsset(Asset_Employee assetEmployee, Asset_Employee assetEmployeeToUpdate);

    void RemoveAssetEmployee(Asset_Employee assetEmployee);
    Task<Asset_Employee> GetAssetEmployeeById(int id);
}