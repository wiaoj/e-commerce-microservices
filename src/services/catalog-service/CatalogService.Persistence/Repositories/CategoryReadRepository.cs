using BuildingBlocks.Persistence.EFCore.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Persistence.Contexts;
using CatalogService.Persistence.Repositories.Interfaces;

namespace CatalogService.Persistence.Repositories;
public sealed class CategoryReadRepository : EFAsyncReadRepository<CategoryEntity, CategoryServiceDbContext>, ICategoryReadRepository {
	public CategoryReadRepository(CategoryServiceDbContext context) : base(context) { }

	public Task<IQueryable<CategoryEntity>> GetCategoriesWithCategoryIds(
		IEnumerable<Guid> categoryIds,
		CancellationToken cancellationToken) {
		cancellationToken.ThrowIfCancellationRequested();
		return this.GetListAsync(new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			OrderBy = x => x.OrderBy(category => category.Name),
			Predicate = x => categoryIds.Any(categoryId => categoryId == x.Id)
		});
	}
}