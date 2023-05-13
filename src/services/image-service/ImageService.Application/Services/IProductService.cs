using ImageService.Domain.Entities;

namespace ImageService.Application.Services;
public interface IProductService {
	public Task AddProduct(ProductEntity product, CancellationToken cancellationToken);
	public Task DeleteProduct(Guid id, CancellationToken cancellationToken);
}