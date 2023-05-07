using CatalogService.Application.Features.Products.Dtos;

namespace CatalogService.Application.Services;
public interface IProductService {
	public Task AddProduct(CreateProductDto createProductDto, CancellationToken cancellationToken);
}