using BicycleCompany.PartModels.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BicycleCompany.PartModels.API.Infrastructure.Configuration
{
    public class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(m => m.Name)
                .HasMaxLength(1024)
                .IsRequired();

            builder.HasMany(x => x.PartModels)
                .WithOne(pm => pm.Part)
                .HasForeignKey(pm => pm.PartId);
        }
    }
}
