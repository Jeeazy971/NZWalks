using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk); 
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllWalkAsync()
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            return await _dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk is null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.Region = walk.Region;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await _dbContext.SaveChangesAsync();

            return existingWalk;

        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
           var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if(existingWalk is null)
            {
                return null;
            }

            _dbContext.Walks.Remove(existingWalk);
            await _dbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
