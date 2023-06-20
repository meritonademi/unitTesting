using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOAProject.DTOs;
using SOAProject.Models;
using SOAProject.Services.AssetService;

namespace SOAProject.Controllers;

[ApiController]
[Route("api/assets")]
public class AssetController : ControllerBase
{
    private readonly IAssetService _assetService;

    public AssetController(IAssetService assetService)
    {
        _assetService = assetService ?? throw new ArgumentNullException(nameof(assetService));
    }

    [Authorize(Policy = "role")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Asset>>> GetAllAssets()
    {
        var items = await _assetService.GetAllAssets();
        return Ok(items);
    }
    [Authorize(Policy = "role")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Asset>> GetAssetById(int id)
    {
        var item = await _assetService.GetAssetById(id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }
    [Authorize(Policy = "role")]
    [HttpPost]
    public async Task<ActionResult<Asset>> CreateCategory(AssetDTO item)
    {
        Asset asset = new Asset()
        {
            Name = item.Name,
            CategoryId = item.CategoryId,
            SerialNr = item.SerialNr
        };
        await _assetService.CreateAsset(asset);
        return Ok(item);
    }

    [Authorize(Policy = "role")]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAssetAsync(int id, Asset asset)
    {
        if (id != asset.Id)
        {
            return BadRequest();
        }

        try
        {
            var message = await _assetService.UpdateAssetAsync(id, asset);
            return Ok(message);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    [Authorize(Policy = "role")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsset(int id)
    {
        var item = await _assetService.GetAssetById(id);
        if (item == null)
        {
            return NotFound();
        }

        var message = await _assetService.DeleteAsset(item.Id);
        return Ok(message);
    }
}