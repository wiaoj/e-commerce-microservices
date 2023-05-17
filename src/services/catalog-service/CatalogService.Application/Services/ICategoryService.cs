using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using CatalogService.Application.Dtos.Requests.Category;
using CatalogService.Application.Dtos.Responses.Category;

namespace CatalogService.Application.Services;
public interface ICategoryService {
	public Task AddCategoryAsync(
		CreateCategoryRequest createCategoryRequest,
		CancellationToken cancellationToken);
	public Task UpdateCategoryAsync(
		UpdateCategoryRequest updateCategoryRequest,
		CancellationToken cancellationToken);
	public Task DeleteCategoryAsync(
		DeleteCategoryRequest deleteCategoryRequest,
		CancellationToken cancellationToken);
	public Task<IPaginate<GetCategoriesResponse>> GetCategoriesAsync(
		PaginationRequest paginationRequest,
		CancellationToken cancellationToken);
	public Task<GetCategoryResponse> GetCategoryAsync(
		CategoryIdRequest categoryIdRequest,
		CancellationToken cancellationToken);
	public Task<GetCategoryWithProductsResponse> GetCategoryWithProductsAsync(
		CategoryIdRequest categoryIdRequest,
		PaginationRequest paginationRequest,
		CancellationToken cancellationToken);
	public Task<IEnumerable<GetRootCategoriesResponse>> GetRootCategoriesResponseAsync(
		CancellationToken cancellationToken);
}