using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOAProject.DTOs;
using SOAProject.Models;
using SOAProject.Services.AssetEmployeeService;

namespace SOAProject.Controllers;

[ApiController]
[Route("api/assets-employee")]
public class AssetEmployeeController : ControllerBase
{
    private readonly IAssetEmployeeService _assetEmployeeService;

    public AssetEmployeeController(IAssetEmployeeService assetEmployeeService)
    {
        _assetEmployeeService = assetEmployeeService ?? throw new ArgumentNullException(nameof(assetEmployeeService));
    }
    [Authorize(Policy = "role")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Asset_Employee>>> GetAllAssetsEmployee()
    {
        var items = await _assetEmployeeService.GetAllAssetEmployees();
        return Ok(items);
    }

    [Authorize(Policy = "role")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Asset_Employee>> GetAssetByEmployeeId(int id)
    {
        var item = await _assetEmployeeService.GetAssetByEmployeeId(id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [Authorize(Policy = "role")]
    [HttpPost]
    public async Task<ActionResult<Asset_Employee>> CreateAssetEmployee(Asset_EmployeeDTO item)
    {
        var i = await _assetEmployeeService.CreateAssetEmployee(item);
        if (i == null)
        {
            return BadRequest("Invalid asset id");
        }

        return Ok(i);
    }

    [Authorize(Policy = "role")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAssetEmployee(int id)
    {
        var item = await _assetEmployeeService.GetAsseEmployeeById(id);
        if (item == null)
        {
            return NotFound();
        }

        var message = await _assetEmployeeService.DeleteAssetEmployee(item.AssetId);
        return Ok(message);
    }
}