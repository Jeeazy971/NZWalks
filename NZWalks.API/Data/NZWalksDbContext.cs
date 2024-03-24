using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {
            
        }

        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("b89b23cd-9c4d-4d74-a17b-71238b536361"),
                    Name = "Easy",
                },
                new Difficulty
                {
                    Id = Guid.Parse("0759ae7d-267b-4996-931f-2a91bd533e10"),
                    Name = "Medium",
                },
                new Difficulty
                {
                    Id = Guid.Parse("ba75d11d-cda0-44ee-b486-682cb13fa63b"),
                    Name = "Hard",
                },
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.Parse("5f49cc7b-d60e-4238-9cae-735950b28dc6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://www.pexels.com/fr-fr/photo/ville-horizon-autoroute-ciel-bleu-5342978/"
                },
                new Region
                {
                    Id = Guid.Parse("09de50b4-aa25-446c-84d9-335b9ef172fb"),
                    Name = "Canada",
                    Code = "CAN",
                    RegionImageUrl = "https://www.pexels.com/fr-fr/photo/batiments-pres-de-plan-d-eau-la-nuit-1519088/"
                },
                new Region
                {
                    Id = Guid.Parse("0201fb76-55a0-4751-bc9a-492c571f4001"),
                    Name = "Japon",
                    Code = "JPN",
                    RegionImageUrl = "https://www.pexels.com/fr-fr/photo/temple-de-la-pagode-pres-du-lac-sous-un-ciel-nuageux-1829980/"
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
