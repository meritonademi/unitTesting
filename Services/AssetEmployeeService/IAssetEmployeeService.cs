using ItemManagementSystem1.DTOs;
using ItemManagementSystem1.Models;

namespace ItemManagementSystem1.Services.AssetEmployeeService;

public interface IAssetEmployeeService
{
    Task<IEnumerable<Asset_EmployeeResponseDTO>> GetAllAssetEmployees();
    Task<Asset_EmployeeResponseDTO> GetAssetByEmployeeId(int id);
    Task<Asset_Employee> CreateAssetEmployee(Asset_EmployeeDTO item);
    Task<string> DeleteAssetEmployee(int id);
    Task<Asset_Employee> GetAsseEmployeeById(int id);
}