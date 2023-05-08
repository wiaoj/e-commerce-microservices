using BuildingBlocks.Persistence.EFCore.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Persistence.Contexts;
using CatalogService.Persistence.Repositories.Interfaces;

namespace CatalogService.Persistence.Repositories;
public sealed class ProductWriteRepository : EFAsyncWriteRepository<ProductEntity, CategoryServiceDbContext>, IProductWriteRepository {
	public ProductWriteRepository(CategoryServiceDbContext context) : base(context) { }
}