using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<List<Walk>> GetAllWalkAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true, int pageNuber = 1, int pageSize = 1000)
        {
            var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Fitre
            if(!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
            }

            // Tri
            if(!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination

            var skipResults = (pageNuber - 1) * pageSize;



            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
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
