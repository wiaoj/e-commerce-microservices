using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Persistence.EntityConfigurations;
internal class ProductVariantEntityConfuguration : IEntityTypeConfiguration<ProductVariantEntity> {
	public void Configure(EntityTypeBuilder<ProductVariantEntity> builder) {
		builder.Property(product => product.Id)
			.ValueGeneratedOnAdd()
			.IsRequired(true);

		builder.HasOne(productVariant => productVariant.Product)
			.WithMany(product => product.ProductVariants)
			.HasForeignKey(productVariant => productVariant.ProductId);
	}
}