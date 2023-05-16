using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Persistence.EntityConfigurations;
internal class VariantTypeOptionEntityConfuguration : IEntityTypeConfiguration<VariantTypeOptionEntity> {
	public void Configure(EntityTypeBuilder<VariantTypeOptionEntity> builder) {
		builder.Property(product => product.Id)
			.ValueGeneratedOnAdd()
			.IsRequired(true);

		builder.Property(variantTypeOption => variantTypeOption.Value)
			.IsRequired(true);

		builder.Property(variantTypeOption => variantTypeOption.VariantTypeId)
			.IsRequired(true);

		builder.HasOne(variantTypeOption => variantTypeOption.VariantType)
			.WithMany(variantType => variantType.VariantTypeOptions)
			.HasForeignKey(variantTypeOption => variantTypeOption.VariantTypeId);
	}
}