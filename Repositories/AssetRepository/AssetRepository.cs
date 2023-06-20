using SOAProject.Data;
using SOAProject.Models;

namespace SOAProject.Repositories.AssetRepository;

public class AssetRepository : IAssetRepository
{
    private readonly AppDbContext _dbContext;

    public AssetRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<Asset>> GetAllAssets()
    {
        List<Asset> assets = new List<Asset>(_dbContext.Assets);

        var fullEntries = _dbContext.Assets.Join(
            _dbContext.Categories,
            asset => asset.CategoryId,
            category => category.Id,
            (asset, category) => new { asset, category }
        );

        foreach (var fullEntry in fullEntries)
        {
            Asset item = new Asset();
            item = fullEntry.asset;
            item.Category = fullEntry.category;
            assets.Add(item);
        }

        return assets;
    }

    public async Task<Asset> GetAssetById(int id)
    {
        var fullEntries = _dbContext.Assets.Join(
            _dbContext.Categories,
            asset => asset.CategoryId,
            category => category.Id,
            (asset, category) => new { asset, category }
        ).Where(fullyEntries => fullyEntries.asset.Id.Equals(id));


        Asset asset = new Asset();
        foreach (var fullEntry in fullEntries)
        {
            asset = fullEntry.asset;
            asset.Category = fullEntry.category;
        }

        if (asset.Id == 0)
        {
            return null;
        }

        return asset;
    }

    public Asset AddAssetAsync(Asset asset)
    {
        _dbContext.Assets.Add(asset);
        _dbContext.SaveChanges();
        return asset;
    }

    public void UpdateAsset(Asset asset, Asset assetToUpdate)
    {
        asset.Id = assetToUpdate.Id;
        asset.Name = assetToUpdate.Name;
        _dbContext.Assets.Update(asset);
        _dbContext.SaveChanges();
    }

    public void RemoveAsset(Asset asset)
    {
        _dbContext.Assets.Remove(asset);
        _dbContext.SaveChanges();
    }
}