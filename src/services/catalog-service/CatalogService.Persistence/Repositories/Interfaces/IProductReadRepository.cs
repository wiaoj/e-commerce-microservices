using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Persistence.EFCore.Repositories.Interfaces;
using CatalogService.Domain.Entities;

namespace CatalogService.Persistence.Repositories.Interfaces;
public interface IProductReadRepository : IEFAsyncReadRepository<ProductEntity> {
	public Task<IPaginate<ProductEntity>> GetProductsByCategoryId(
		Guid categoryId,
		PaginationRequest paginationRequest,
		CancellationToken cancellationToken);
}