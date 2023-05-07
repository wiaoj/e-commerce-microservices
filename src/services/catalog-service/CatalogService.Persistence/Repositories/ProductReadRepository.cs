using BuildingBlocks.Persistence.EFCore.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Persistence.Contexts;
using CatalogService.Persistence.Repositories.Interfaces;

namespace CatalogService.Persistence.Repositories;
public sealed class ProductReadRepository : EFAsyncReadRepository<ProductEntity, CategoryServiceDbContext>, IProductReadRepository {
	public ProductReadRepository(CategoryServiceDbContext context) : base(context) { }
}