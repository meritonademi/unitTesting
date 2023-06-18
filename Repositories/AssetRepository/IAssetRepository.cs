using ItemManagementSystem1.Models;

namespace ItemManagementSystem1.Repositories.AssetRepository;

public interface IAssetRepository
{
    void RemoveAsset(Asset asset);

    void UpdateAsset(Asset asset, Asset assetToUpdate);

    Task AddAssetAsync(Asset asset);

    Task<Asset> GetAssetById(int id);

    Task<IEnumerable<Asset>> GetAllAssets();
}