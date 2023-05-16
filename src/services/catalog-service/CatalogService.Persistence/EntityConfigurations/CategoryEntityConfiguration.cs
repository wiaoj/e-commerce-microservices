using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Persistence.EntityConfigurations;
internal class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity> {
	public void Configure(EntityTypeBuilder<CategoryEntity> builder) {
		builder.Property(category => category.Id)
			.ValueGeneratedOnAdd()
			.IsRequired(true);

		builder.HasOne(category => category.ParentCategory)
			.WithMany(category => category.ChildCategories)
			.HasForeignKey(category => category.ParentCategoryId);

		builder.HasMany(category => category.Products)
			.WithMany(product => product.Categories)
			.UsingEntity<CategoryEntityProductEntity>();
	}
}