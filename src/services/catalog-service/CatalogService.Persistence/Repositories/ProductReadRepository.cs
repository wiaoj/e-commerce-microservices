using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Persistence.EFCore.Parameters;
using BuildingBlocks.Persistence.EFCore.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Persistence.Contexts;
using CatalogService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Persistence.Repositories;
public sealed class ProductReadRepository : EFAsyncReadRepository<ProductEntity, CategoryServiceDbContext>, IProductReadRepository {
	public ProductReadRepository(CategoryServiceDbContext context) : base(context) { }

	public Task<IPaginate<ProductEntity>> GetProductsByCategoryId(Guid categoryId, CancellationToken cancellationToken) {
		GetPaginatedListParameters<ProductEntity> parameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			OrderBy = x => x.OrderBy(x => x.Name),
			PaginationOptions = new(),
			Include = x => x.Include(x => x.Categories),
			Predicate = x => x.Categories.Any(c => c.Id == categoryId)
		};
		return this.GetPaginatedListAsync(parameters);
	}
}