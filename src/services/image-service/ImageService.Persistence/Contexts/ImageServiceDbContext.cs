using BuildingBlocks.Domain;
using BuildingBlocks.Persistence.EFCore.MSSQL;
using ImageService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageService.Persistence.Contexts;
public sealed class ImageServiceDbContext : MSSQLDbContext {
	public ImageServiceDbContext(DbContextOptions options) : base(options) { }

	public DbSet<ProductEntity> Products { get; set; }
	public DbSet<ProductImageEntity> ProductImages { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<ProductEntity>()
				.Ignore(product => product.CreatedAt)
				.Ignore(product => product.UpdatedAt)
				.HasMany(product => product.ProductImages)
				.WithOne(productImage => productImage.Product)
				.HasForeignKey(productImage => productImage.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<ProductImageEntity>()
				//.Ignore(productImage => productImage.CreatedAt)
				.Ignore(productImage => productImage.UpdatedAt)
				.HasOne(productImage => productImage.Product)
				.WithMany(product => product.ProductImages)
				.HasForeignKey(productImage => productImage.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

		base.OnModelCreating(modelBuilder);
	}
}