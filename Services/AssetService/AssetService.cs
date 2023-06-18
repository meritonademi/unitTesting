using ItemManagementSystem1.Models;
using ItemManagementSystem1.Repositories.AssetRepository;

namespace ItemManagementSystem1.Services.AssetService;

public class AssetService : IAssetService
{
    private readonly IAssetRepository _assetRepository;

    public AssetService(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public async Task<IEnumerable<Asset>> GetAllAssets()
    {
        var items = await _assetRepository.GetAllAssets();
        return items;
    }

    public async Task<Asset> GetAssetById(int id)
    {
        var item = await _assetRepository.GetAssetById(id);
        return item;
    }

    public async Task<Asset> CreateAsset(Asset item)
    {
        try
        {
            _assetRepository.AddAssetAsync(item);
            return item;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> UpdateAssetAsync(int id, Asset asset)
    {
        try
        {
            var item = await _assetRepository.GetAssetById(id);
            if (item == null)
            {
                throw new ArgumentException("Asset not found.");
            }

            _assetRepository.UpdateAsset(item, asset);
            return "Successfully update";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> DeleteAsset(int id)
    {
        var item = await _assetRepository.GetAssetById(id);
        if (item == null)
        {
            throw new ArgumentException("Asset not found.");
        }

        _assetRepository.RemoveAsset(item);
        return "Successfully delete";
    }
}