using BuildingBlocks.Persistence.EFCore.Parameters;
using ImageService.Application.Services;
using ImageService.Domain.Entities;
using ImageService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImageService.Persistence.Services;
internal sealed class ProductService : IProductService {
	private readonly IProductWriteRepository productWriteRepository;
	private readonly IProductReadRepository productReadRepository;
	private readonly IProductImageService productImageService;

	public ProductService(
		IProductWriteRepository productRepository,
		IProductReadRepository productReadRepository,
		IProductImageService productImageService) {
		this.productWriteRepository = productRepository;
		this.productReadRepository = productReadRepository;
		this.productImageService = productImageService;
	}

	public async Task AddProduct(ProductEntity product, CancellationToken cancellationToken) {
		await this.productWriteRepository.AddAsync(product, cancellationToken);
		await this.productWriteRepository.SaveChangesAsync(cancellationToken);
	}

	public async Task DeleteProduct(Guid id, CancellationToken cancellationToken) {
		GetParameters<ProductEntity> parameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			Predicate = product => product.Id == id,
			Include = x => x.Include(product => product.ProductImages)
		};

		ProductEntity? productEntity = await this.productReadRepository.GetAsync(parameters);

		if(productEntity is null)
			return;

		await this.productWriteRepository.DeleteAsync(productEntity, cancellationToken);
		await this.productWriteRepository.SaveChangesAsync(cancellationToken);

		IEnumerable<Guid>? imageIds = productEntity.ProductImages.Select(images => images.Id);

		await this.productImageService.DeleteImages(id, imageIds);
	}
}