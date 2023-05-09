using AutoMapper;
using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Persistence.EFCore.Parameters;
using CatalogService.Application.Dtos.Requests.Category;
using CatalogService.Application.Dtos.Requests.Product;
using CatalogService.Application.Dtos.Responses.Product;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Persistence.Repositories.Interfaces;

namespace CatalogService.Persistence.Services;
internal sealed class ProductService : IProductService {
	private readonly IProductWriteRepository productWriteRepository;
	private readonly IProductReadRepository productReadRepository;
	private readonly ICategoryReadRepository categoryReadRepository;
	private readonly IMapper mapper;

	public ProductService(IProductWriteRepository productWriteRepository,
					   IProductReadRepository productReadRepository,
					   IMapper mapper,
					   ICategoryReadRepository categoryReadRepository) {
		this.productWriteRepository = productWriteRepository;
		this.productReadRepository = productReadRepository;
		this.mapper = mapper;
		this.categoryReadRepository = categoryReadRepository;
	}

	public async Task AddProductAsync(
		CreateProductRequest createProductRequest,
		CancellationToken cancellationToken) {
		ProductEntity product = this.mapper.Map<ProductEntity>(createProductRequest);

		await this.AddProductCategoriesIfExistsAsync(createProductRequest.CategoryIds, product, cancellationToken);

		Task.WaitAll(new Task[2] {
			this.productWriteRepository.AddAsync(product, cancellationToken).AsTask(),
			this.productWriteRepository.SaveChangesAsync(cancellationToken)
		}, cancellationToken);
	}

	public async Task DeleteProductAsync(DeleteProductRequest deleteProductRequest, CancellationToken cancellationToken) {
		GetParameters<ProductEntity> parameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = true,
			Predicate = x => x.Id == deleteProductRequest.Id,
		};
		ProductEntity? product = await this.productReadRepository.GetAsync(parameters);
		ArgumentNullException.ThrowIfNull(product, "Ürün bulunamadı!");

		Task.WaitAll(new Task[2] {
			this.productWriteRepository.DeleteAsync(product, cancellationToken).AsTask(),
			this.productWriteRepository.SaveChangesAsync(cancellationToken)
		}, cancellationToken);
	}

	public async Task<IPaginate<GetProductResponse>> GetProductsByCategoryId(
		CategoryIdRequest categoryIdRequest,
		CancellationToken cancellationToken) {
		IPaginate<ProductEntity> products = 
			await this.productReadRepository.GetProductsByCategoryId(
				categoryIdRequest.Value,
				cancellationToken);
		return this.mapper.Map<Paginate<GetProductResponse>>(products);
	}

	public async Task UpdateProductAsync(
		UpdateProductRequest updateProductRequest,
		CancellationToken cancellationToken) {
		ProductEntity? product = await this.productReadRepository.GetAsync(new() {
			CancellationToken = cancellationToken,
			EnableTracking = true,
			Predicate = x => x.Id == updateProductRequest.Id,
		});
		ArgumentNullException.ThrowIfNull(product, "Ürün bulunamadı!");

		ProductEntity updatedCategory = this.mapper.Map(updateProductRequest, product);

		await this.AddProductCategoriesIfExistsAsync(
			updateProductRequest.CategoryIds,
			updatedCategory,
			cancellationToken);

		Task.WaitAll(new Task[2] {
			this.productWriteRepository.UpdateAsync(updatedCategory, cancellationToken).AsTask(),
			this.productWriteRepository.SaveChangesAsync(cancellationToken),
		}, cancellationToken);
	}

	private async Task AddProductCategoriesIfExistsAsync(
		IEnumerable<Guid>? categoryIds,
		ProductEntity product,
		CancellationToken cancellationToken) {
		if(categoryIds?.Any() is true) {
			IQueryable<CategoryEntity> categories = await this.categoryReadRepository.GetCategoriesWithCategoryIds(categoryIds, cancellationToken);
			ArgumentNullException.ThrowIfNull(categories, "Kategori bulunamadı!");
			product.AddRangeCategories(categories);
		}
	}
}
