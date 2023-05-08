using AutoMapper;
using BuildingBlocks.Persistence.EFCore.Parameters;
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

	public async Task AddProduct(CreateProductDto createProductDto, CancellationToken cancellationToken) {
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
}
