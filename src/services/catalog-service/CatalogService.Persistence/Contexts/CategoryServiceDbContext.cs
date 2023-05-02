using BuildingBlocks.Persistence.EFCore.MSSQL;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Persistence.Contexts;
public sealed class CategoryServiceDbContext : MSSQLDbContext {
	public CategoryServiceDbContext(DbContextOptions options) : base(options) { }

	public DbSet<CategoryEntity> Categories { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<CategoryEntity>()
			  .HasOne(c => c.ParentCategory)
			  .WithMany(c => c.ChildCategories)
			  .HasForeignKey(c => c.ParentCategoryId)
			  .OnDelete(DeleteBehavior.SetNull);

		base.OnModelCreating(modelBuilder);
	}
}