using BuildingBlocks.Persistence.EFCore.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Persistence.Contexts;
using CatalogService.Persistence.Repositories.Interfaces;

namespace CatalogService.Persistence.Repositories;
public sealed class CategoryWriteRepository : EFAsyncWriteRepository<CategoryEntity, CategoryServiceDbContext>, ICategoryWriteRepository {
	public CategoryWriteRepository(CategoryServiceDbContext context) : base(context) { }
}