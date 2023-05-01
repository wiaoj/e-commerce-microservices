using BuildingBlocks.Persistence.EFCore.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Persistence.Contexts;
using CatalogService.Persistence.Repositories.Interfaces;

namespace CatalogService.Persistence.Repositories;
public sealed class CategoryReadRepository : EFAsyncReadRepository<CategoryEntity, CategoryServiceDbContext>, ICategoryReadRepository {
	public CategoryReadRepository(CategoryServiceDbContext context) : base(context) { }
}