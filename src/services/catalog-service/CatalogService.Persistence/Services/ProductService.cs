using AutoMapper;
using BuildingBlocks.Persistence.EFCore.Parameters;
using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Application.Features.Products.Dtos;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
		CreateProductDto createProductDto,
		CancellationToken cancellationToken) {
		ProductEntity product = this.mapper.Map<ProductEntity>(createProductDto);

		await AddProductCategoriesIfExistsAsync(createProductDto.CategoryIds, product, cancellationToken);

		Task.WaitAll(new Task[2] {
			this.productWriteRepository.AddAsync(product, cancellationToken).AsTask(),
			this.productWriteRepository.SaveChangesAsync(cancellationToken)
		}, cancellationToken);
	}

	public async Task DeleteProductAsync(DeleteProductDto deleteProductDto, CancellationToken cancellationToken) {
		GetParameters<ProductEntity> parameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = true,
			Predicate = x => x.Id == deleteProductDto.Id,
		};
		ProductEntity? product = await this.productReadRepository.GetAsync(parameters);
		ArgumentNullException.ThrowIfNull(product, "Ürün bulunamadı!");

		Task.WaitAll(new Task[2] {
			this.productWriteRepository.DeleteAsync(product, cancellationToken).AsTask(),
			this.productWriteRepository.SaveChangesAsync(cancellationToken)
		}, cancellationToken);
	}

	public async Task UpdateProductAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken) {
		ProductEntity? product = await this.productReadRepository.GetAsync(new() {
			CancellationToken = cancellationToken,
			EnableTracking = true,
			Predicate = x => x.Id == updateProductDto.Id,
		});
		ArgumentNullException.ThrowIfNull(product, "Ürün bulunamadı!");

		ProductEntity updatedCategory = this.mapper.Map(updateProductDto, product);

		await AddProductCategoriesIfExistsAsync(updateProductDto.CategoryIds, updatedCategory, cancellationToken);

		Task.WaitAll(new Task[2] {
			this.productWriteRepository.UpdateAsync(updatedCategory, cancellationToken).AsTask(),
			this.productWriteRepository.SaveChangesAsync(cancellationToken),
		}, cancellationToken);
	}

	private async Task AddProductCategoriesIfExistsAsync(IEnumerable<Guid>? categoryIds, ProductEntity product, CancellationToken cancellationToken) {
		if(categoryIds?.Any() is true) {
			GetListParameters<CategoryEntity> parameters = new() {
				CancellationToken = cancellationToken,
				EnableTracking = true,
				//Predicate = category => updateProductDto.Categories.SelectMany(x => x.Id == category.Id)
				Predicate = x => categoryIds!.Contains(x.Id)
			};

			//IQueryable<CategoryEntity> categories = await this.categoryReadRepository.GetCategoriesWithCategoryIds(updateProductDto.CategoryIds, cancellationToken);
			IQueryable<CategoryEntity> categories = await this.categoryReadRepository.GetListAsync(parameters);
			ArgumentNullException.ThrowIfNull(categories, "Kategori bulunamadı!");
			product.AddRangeCategories(categories);
		}
	}
}
