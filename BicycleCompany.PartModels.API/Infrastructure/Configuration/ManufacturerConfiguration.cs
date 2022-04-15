using BicycleCompany.PartModels.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BicycleCompany.PartModels.API.Infrastructure.Configuration
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
	{
		public void Configure(EntityTypeBuilder<Manufacturer> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Name)
				.HasMaxLength(1024)
				.IsRequired();

			builder.HasMany(x => x.PartModels)
				.WithOne(pm => pm.Manufacturer)
				.HasForeignKey(x => x.ManufacturerId);
		}
	}
}
