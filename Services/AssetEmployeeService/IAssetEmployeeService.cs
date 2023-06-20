using SOAProject.DTOs;
using SOAProject.Models;

namespace SOAProject.Services.AssetEmployeeService;

public interface IAssetEmployeeService
{
    Task<IEnumerable<Asset_EmployeeResponseDTO>> GetAllAssetEmployees();
    Task<Asset_EmployeeResponseDTO> GetAssetByEmployeeId(int id);
    Task<Asset_Employee> CreateAssetEmployee(Asset_EmployeeDTO item);
    Task<string> DeleteAssetEmployee(int id);
    Task<Asset_Employee> GetAsseEmployeeById(int id);
}