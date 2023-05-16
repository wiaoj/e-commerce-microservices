using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CatalogService.Persistence.EntityConfigurations;
internal class ProductEntityConfuguration : IEntityTypeConfiguration<ProductEntity> {
	public void Configure(EntityTypeBuilder<ProductEntity> builder) {
		builder.Property(product => product.Id)
			.ValueGeneratedOnAdd()
			.IsRequired(true);

		builder.HasMany(p => p.Categories)
			.WithMany(c => c.Products)
			.UsingEntity<CategoryEntityProductEntity>();

		builder.HasMany(p => p.ProductVariants)
			.WithOne(pv => pv.Product)
			.HasForeignKey(pv => pv.ProductId);
	}
}