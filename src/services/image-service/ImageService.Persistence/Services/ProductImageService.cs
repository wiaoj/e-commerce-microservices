using BuildingBlocks.Persistence.EFCore.Parameters;
using ImageService.Application.Dtos.Requests;
using ImageService.Application.Services;
using ImageService.Application.Storage;
using ImageService.Domain.Entities;
using ImageService.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ImageService.Persistence.Services;
internal class ProductImageService : IProductImageService {
	private readonly IStorageService storageService;
	private readonly IProductImageReadRepository productImageReadRepository;
	private readonly IProductImageWriteRepository productImageWriteRepository;
	private readonly IProductReadRepository productReadRepository;

	public ProductImageService(
		IStorageService storageService,
		IProductImageReadRepository productImageReadRepository,
		IProductImageWriteRepository productImageWriteRepository,
		IProductReadRepository productReadRepository) {
		this.storageService = storageService;
		this.productImageReadRepository = productImageReadRepository;
		this.productImageWriteRepository = productImageWriteRepository;
		this.productReadRepository = productReadRepository;
	}

	public Task DeleteImage(Guid productId, Guid imageId) {
		throw new NotImplementedException();
	}

	public Task DeleteImages(Guid productId, IEnumerable<Guid> imageIds) {
		throw new NotImplementedException();
	}

	public async Task SetShowcase(Guid productId, Guid imageId, CancellationToken cancellationToken) {
		GetParameters<ProductImageEntity> findProductShowcaseImageParameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = true,
			Predicate = x => x.ProductId == productId && x.Showcase
		};

		ProductImageEntity? productShowcaseImageEntity =
			await this.productImageReadRepository.GetAsync(findProductShowcaseImageParameters);

		if(productShowcaseImageEntity is not null) {
			productShowcaseImageEntity.Showcase = true;
		}

		GetParameters<ProductImageEntity> parameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = true,
			Predicate = x => x.Id == imageId
		};

		ProductImageEntity? productImageEntity =
			await this.productImageReadRepository.GetAsync(parameters);
		if(productImageEntity is not null) {
			productImageEntity.Showcase = true;
		}

		await this.productImageWriteRepository.SaveChangesAsync(cancellationToken);
	}

	public async Task UploadImages(UploadImageRequest uploadImageRequest, CancellationToken cancellationToken) {
		GetParameters<ProductEntity> requestedProductParameters = new() {
			CancellationToken = cancellationToken,
			Predicate = x => x.Id == uploadImageRequest.ProductId,
			Include = x => x.Include(product => product.ProductImages)
		};
		ProductEntity? requestedProduct =
			await this.productReadRepository.GetAsync(requestedProductParameters);
		ArgumentNullException.ThrowIfNull(requestedProduct, "Ürün bulunamadı");

		List<(String fileName, String path)> uploadedImages =
			await this.storageService.UploadAsync(
				$"product/media/images/{requestedProduct.Id}",
				uploadImageRequest.Images);

		List<ProductImageEntity> uploadedProductImages =
			uploadedImages.ConvertAll(image => new ProductImageEntity {
				Name = image.fileName,
				Path = image.path,
				ProductId = requestedProduct.Id,
				Showcase = default
			});

		Boolean isShowcase = requestedProduct.ProductImages.Any(productImage => productImage.Showcase);
		if(isShowcase is false)
			uploadedProductImages.First().Showcase = true;

		await this.productImageWriteRepository.AddRangeAsync(uploadedProductImages, cancellationToken);
		await this.productImageWriteRepository.SaveChangesAsync(cancellationToken);
	}
}
