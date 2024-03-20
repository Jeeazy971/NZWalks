using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
           return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetRegionByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion is null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await _dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(existingRegion is null)
            {
                return null;
            }

            _dbContext.Remove(existingRegion);
            await _dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
