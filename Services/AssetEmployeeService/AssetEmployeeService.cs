using Microsoft.AspNetCore.Mvc;
using SOAProject.DTOs;
using SOAProject.Models;
using SOAProject.Repositories.AssetEmployeeRepository;

namespace SOAProject.Services.AssetEmployeeService;

public class AssetEmployeeService : IAssetEmployeeService
{
    private readonly IAssetEmployeeRepository _assetEmployeeRepository;

    public AssetEmployeeService(IAssetEmployeeRepository assetEmployeeRepository)
    {
        _assetEmployeeRepository = assetEmployeeRepository;
    }

    public async Task<IEnumerable<Asset_EmployeeResponseDTO>> GetAllAssetEmployees()
    {
        var items = await _assetEmployeeRepository.GetAllAssetsEmployee();
        return items;
    }

    public async Task<Asset_EmployeeResponseDTO> GetAssetByEmployeeId(int id)
    {
        var item = await _assetEmployeeRepository.GetAssetEmployeeByEmployeeId(id);
        return item;
    }

    public async Task<Asset_Employee> GetAsseEmployeeById(int id)
    {
        var item = await _assetEmployeeRepository.GetAssetEmployeeById(id);
        return item;
    }

    public async Task<Asset_Employee> CreateAssetEmployee(Asset_EmployeeDTO item)
    {
        try
        {
            Asset_Employee assetEmployee = new Asset_Employee();
            assetEmployee.EmployeeId = item.EmployeeId;
            assetEmployee.AssetId = item.AssetId;

            var i = _assetEmployeeRepository.GetAssetEmployeeById(item.AssetId);
            if (i.Result != null)
            {
                return null;
            }

            _assetEmployeeRepository.AddAssetEmployeeAsync(assetEmployee);
            return assetEmployee;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public async Task<string> DeleteAssetEmployee(int id)
    {
        var item = await _assetEmployeeRepository.GetAssetEmployeeById(id);
        if (item == null)
        {
            throw new ArgumentException("Asset not found.");
        }

        Asset_Employee assetEmployee = new Asset_Employee();
        assetEmployee.AssetId = item.AssetId;
        assetEmployee.EmployeeId = item.EmployeeId;
        _assetEmployeeRepository.RemoveAssetEmployee(assetEmployee);
        return "Successfully delete";
    }
}