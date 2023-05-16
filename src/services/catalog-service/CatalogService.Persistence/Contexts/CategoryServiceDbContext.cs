using BuildingBlocks.Persistence.EFCore.MSSQL;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CatalogService.Persistence.Contexts;
public sealed class CategoryServiceDbContext : MSSQLDbContext {
	public CategoryServiceDbContext(DbContextOptions options) : base(options) { }

	public DbSet<CategoryEntity> Categories { get; set; }
	public DbSet<ProductEntity> Products { get; set; }
	//public DbSet<ProductVariantEntity> ProductVariants { get; set; }
	//public DbSet<VariantTypeEntity> VariantTypes { get; set; }
	//public DbSet<VariantTypeOptionEntity> VariantTypeOptions { get; set; }
	public DbSet<CategoryEntityProductEntity> CategoriesProducts { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);
	}
}