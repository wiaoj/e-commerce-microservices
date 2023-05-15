using BuildingBlocks.Persistence.EFCore.Parameters;
using ImageService.Application.Dtos.Requests;
using ImageService.Application.Dtos.Responses;
using ImageService.Application.Extensions;
using ImageService.Application.Services;
using ImageService.Application.Storage;
using ImageService.Domain.Entities;
using ImageService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImageService.Persistence.Services;
internal class ProductImageService : IProductImageService {
	private readonly ILocalStorage storageService;
	private readonly IProductImageReadRepository productImageReadRepository;
	private readonly IProductImageWriteRepository productImageWriteRepository;
	private readonly IProductReadRepository productReadRepository;

	public ProductImageService(
		ILocalStorage storageService,
		IProductImageReadRepository productImageReadRepository,
		IProductImageWriteRepository productImageWriteRepository,
		IProductReadRepository productReadRepository) {
		this.storageService = storageService;
		this.productImageReadRepository = productImageReadRepository;
		this.productImageWriteRepository = productImageWriteRepository;
		this.productReadRepository = productReadRepository;
	}

	public async Task DeleteImage(Guid productId, Guid imageId, CancellationToken cancellationToken) {
		GetParameters<ProductEntity> productParameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			Predicate = x => x.Id == productId
		};

		ProductEntity? productEntity = await this.productReadRepository.GetAsync(productParameters);
		ArgumentNullException.ThrowIfNull(productEntity, "Ürün bulunamadı!");

		GetParameters<ProductImageEntity> productImageParameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			Predicate = x => x.ProductId == productId
		};

		ProductImageEntity? productImage = await this.productImageReadRepository.GetAsync(productImageParameters);
		ArgumentNullException.ThrowIfNull(productImage, "Resim bulunamadı!");

		String path = productEntity.Id.WithProductMediaImagesPath();
		await this.storageService.DeleteAsync(path, productImage.Name);

		await this.productImageWriteRepository.DeleteAsync(productImage, cancellationToken);
		await this.productImageWriteRepository.SaveChangesAsync(cancellationToken);

		IQueryable<ProductImageEntity> productImagesQueryable = await this.productImageReadRepository.GetListAsync(new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			Predicate = x => x.ProductId == productId
		});

		if(productImagesQueryable.Any() is false) {
			await this.storageService.DeletePath(path);
		}
		await Task.CompletedTask;
	}

	public async Task DeleteImages(Guid productId, IEnumerable<Guid> imageIds, CancellationToken cancellationToken) {
		GetParameters<ProductEntity> productParameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			Predicate = x => x.Id == productId
		};

		ProductEntity? productEntity = await this.productReadRepository.GetAsync(productParameters);
		ArgumentNullException.ThrowIfNull(productEntity, "Ürün bulunamadı!");

		GetListParameters<ProductImageEntity> productImageParameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			Predicate = x => x.ProductId == productId && imageIds.Any(imageId => imageId == x.Id)
		};

		IQueryable<ProductImageEntity> productImagesQueryable = await this.productImageReadRepository.GetListAsync(productImageParameters);

		if(productImagesQueryable.Any() is false || productImagesQueryable.Count() != imageIds.Count()) {
			throw new Exception("Ürün görseli bulunamadı");
		}

		String path = productEntity.Id.WithProductMediaImagesPath();

		foreach(ProductImageEntity productImage in productImagesQueryable) {
			await this.storageService.DeleteAsync(path, productImage.Name);
		}


		await this.productImageWriteRepository.DeleteRangeAsync(productImagesQueryable, cancellationToken);
		await this.productImageWriteRepository.SaveChangesAsync(cancellationToken);

		productImagesQueryable = await this.productImageReadRepository.GetListAsync(new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			Predicate = x => x.ProductId == productId
		});

		if(productImagesQueryable.Any() is false) {
			await this.storageService.DeletePath(path);
		}
		await Task.CompletedTask;
	}

	public async Task<IEnumerable<ProductImagesResponse>> GetImagesByProductId(Guid productId, CancellationToken cancellationToken) {
		GetListParameters<ProductImageEntity> parameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			OrderBy = x => x.OrderBy(productImage => productImage.Showcase),
			Predicate = x => x.ProductId == productId
		};

		IQueryable<ProductImageEntity> productImages = 
			await this.productImageReadRepository.GetListAsync(parameters);

		return productImages.ToList().ConvertAll(productImage => new ProductImagesResponse {
			ProductId = productImage.ProductId,
			Path = productImage.Path,
			Name = productImage.Name,
			Showcase = productImage.Showcase
		});
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
				requestedProduct.Id.WithProductMediaImagesPath(),
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
