using ItemManagementSystem1.Models;
using ItemManagementSystem1.Services.AssetService;
using Microsoft.AspNetCore.Mvc;

namespace ItemManagementSystem1.Controllers;

[ApiController]
[Route("api/assets")]
public class AssetController : ControllerBase
{
    private readonly IAssetService _assetService;

    public AssetController(IAssetService assetService)
    {
        _assetService = assetService ?? throw new ArgumentNullException(nameof(assetService));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Asset>>> GetAllAssets()
    {
        var items = await _assetService.GetAllAssets();
        return Ok(items);
    }

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

    [HttpPost]
    public async Task<ActionResult<Asset>> CreateCategory(Asset item)
    {
        await _assetService.CreateAsset(item);
        return Ok(item);
    }


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