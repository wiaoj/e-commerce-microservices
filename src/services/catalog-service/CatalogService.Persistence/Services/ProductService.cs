using AutoMapper;
using BuildingBlocks.Persistence.EFCore.Parameters;
using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Application.Features.Products.Dtos;
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

	public async Task AddProductAsync(CreateProductDto createProductDto, CancellationToken cancellationToken) {
		CategoryEntity? category = null;
		if(createProductDto.CategoryId is not null) {
			GetParameters<CategoryEntity> parameters = new() {
				CancellationToken = cancellationToken,
				EnableTracking = true,
				Predicate = x => x.Id == createProductDto.CategoryId,
			};
			category = await this.categoryReadRepository.GetAsync(parameters);

			ArgumentNullException.ThrowIfNull(category, "Kategori bulunamadı");
		}
		

		ProductEntity product = this.mapper.Map<ProductEntity>(createProductDto);

		if(category is not null) {
			product.AddCategory(category);
		}

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

		if(updateProductDto.Categories.Any()) {
			//GetParameters<ProductEntity> parameters = new() {
			//	CancellationToken = cancellationToken,
			//	EnableTracking = true,
			//	Predicate = x => x.Id == updateProductDto.ParentCategoryId,
			//};
			GetListParameters<ProductEntity> parameters = new() {
				CancellationToken = cancellationToken,
				EnableTracking = true,
				Predicate = x => x.Categories.SelectMany(x => x.Id == updateProductDto.Categories.SelectMany())
			};

			ProductEntity? parentCategory = await this.categoryReadRepository.GetListAsync(parameters);
			ArgumentNullException.ThrowIfNull(parentCategory, "Üst kategori bulunamadı!");
			//product.SetParentCategory(parentCategory);
		}

		//ProductEntity updatedCategory = this.mapper.Map(updateCategoryDto, product);
		//Task.WaitAll(new Task[2] {
		//	this.categoryWriteRepository.UpdateAsync(updatedCategory, cancellationToken).AsTask(),
		//	this.categoryWriteRepository.SaveChangesAsync(cancellationToken),
		//}, cancellationToken);
	}
}
