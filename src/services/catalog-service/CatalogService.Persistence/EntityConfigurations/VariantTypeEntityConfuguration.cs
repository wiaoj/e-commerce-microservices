using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Persistence.EntityConfigurations;
internal class VariantTypeEntityConfuguration : IEntityTypeConfiguration<VariantTypeEntity> {
	public void Configure(EntityTypeBuilder<VariantTypeEntity> builder) {
		builder.Property(product => product.Id)
			.ValueGeneratedOnAdd()
			.IsRequired(true);

		builder.Property(variantType => variantType.Name)
			.IsRequired(true);

		builder.HasMany(variantType => variantType.VariantTypeOptions)
			.WithOne(variantTypeOption => variantTypeOption.VariantType)
			.HasForeignKey(variantTypeOption => variantTypeOption.VariantTypeId);

		//builder.HasData(new VariantTypeEntity("Beden") {
		//	Id = Guid.NewGuid(),
		//}, new VariantTypeEntity("Renk") {
		//	Id = Guid.NewGuid()
		//});
	}
}