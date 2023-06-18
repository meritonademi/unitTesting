using ItemManagementSystem1.Models;

namespace ItemManagementSystem1.Services.AssetService;

public interface IAssetService
{
    Task<string> DeleteAsset(int id);
    Task<string> UpdateAssetAsync(int id, Asset asset);
    Task<Asset> CreateAsset(Asset item);
    Task<IEnumerable<Asset>> GetAllAssets();
    Task<Asset> GetAssetById(int id);
}