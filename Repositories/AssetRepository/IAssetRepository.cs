using SOAProject.Models;

namespace SOAProject.Repositories.AssetRepository;

public interface IAssetRepository
{
    void RemoveAsset(Asset asset);

    void UpdateAsset(Asset asset, Asset assetToUpdate);

    Asset AddAssetAsync(Asset asset);

    Task<Asset> GetAssetById(int id);

    Task<IEnumerable<Asset>> GetAllAssets();
}