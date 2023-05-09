using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using CatalogService.Application.Dtos.Requests.Category;
using CatalogService.Application.Dtos.Requests.Product;
using CatalogService.Application.Dtos.Responses.Product;

namespace CatalogService.Application.Services;
public interface IProductService {
	public Task AddProductAsync(
		CreateProductRequest createProductRequest,
		CancellationToken cancellationToken);
	public Task DeleteProductAsync(
		DeleteProductRequest deleteProductRequest,
		CancellationToken cancellationToken);
	public Task UpdateProductAsync(
		UpdateProductRequest updateProductRequest,
		CancellationToken cancellationToken);
	public Task<IPaginate<GetProductResponse>> GetProductsByCategoryId(
		CategoryIdRequest categoryId,
		PaginationRequest paginationRequest,
		CancellationToken cancellationToken);
}