using BuildingBlocks.Persistence.EFCore.MSSQL;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Persistence.Contexts;
public sealed class CategoryServiceDbContext : MSSQLDbContext {
	public CategoryServiceDbContext(DbContextOptions options) : base(options) { }

	public DbSet<CategoryEntity> Categories { get; set; }
	public DbSet<ProductEntity> Products { get; set; }
	public DbSet<CategoryEntityProductEntity> CategoriesProducts { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<CategoryEntity>()
			  .HasOne(c => c.ParentCategory)
			  .WithMany(c => c.ChildCategories)
			  .HasForeignKey(c => c.ParentCategoryId);

		modelBuilder.Entity<CategoryEntity>()
			  .HasMany(c => c.Products)
			  .WithMany(p => p.Categories)
			  .UsingEntity<CategoryEntityProductEntity>();

		modelBuilder.Entity<ProductEntity>()
			  .HasMany(p => p.Categories)
			  .WithMany(c => c.Products)
			  .UsingEntity<CategoryEntityProductEntity>();

		base.OnModelCreating(modelBuilder);
	}
}