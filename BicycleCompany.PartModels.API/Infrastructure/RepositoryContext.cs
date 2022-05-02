using BicycleCompany.PartModels.API.Infrastructure.Configuration;
using BicycleCompany.PartModels.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BicycleCompany.PartModels.API.Infrastructure
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartModel> PartModels { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PartConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
            modelBuilder.ApplyConfiguration(new PartModelConfiguration());
        }
    }

}
