using ItemManagementSystem1.Data;
using ItemManagementSystem1.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemManagementSystem1.Repositories.AssetRepository;

public class AssetRepository : IAssetRepository
{
    private readonly AppDbContext _dbContext;

    public AssetRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<Asset>> GetAllAssets()
    {
        return await _dbContext.Assets.ToListAsync();
    }

    public async Task<Asset> GetAssetById(int id)
    {
        return await _dbContext.Assets.FindAsync(id);
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