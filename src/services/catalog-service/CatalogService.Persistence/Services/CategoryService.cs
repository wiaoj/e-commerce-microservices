using AutoMapper;
using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Persistence.EFCore.Parameters;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using CatalogService.Application.Dtos.Responses.Category;
using CatalogService.Application.Dtos.Requests.Category;
using CatalogService.Application.Dtos.Responses.Product;

namespace CatalogService.Persistence.Services;
internal sealed class CategoryService : ICategoryService {
	private readonly ICategoryWriteRepository categoryWriteRepository;
	private readonly ICategoryReadRepository categoryReadRepository;
	private readonly IMapper mapper;
	private readonly IProductReadRepository productReadRepository;

	public CategoryService(ICategoryWriteRepository categoryWriteRepository,
						ICategoryReadRepository categoryReadRepository,
						IMapper mapper,
						IProductReadRepository productReadRepository) {
		this.categoryWriteRepository = categoryWriteRepository;
		this.categoryReadRepository = categoryReadRepository;
		this.mapper = mapper;
		this.productReadRepository = productReadRepository;
	}

	public async Task AddCategoryAsync(
		CreateCategoryRequest createCategoryRequest,
		CancellationToken cancellationToken) {
		if(createCategoryRequest.ParentCategoryId is not null) {
			GetParameters<CategoryEntity> parameters = new() {
				CancellationToken = cancellationToken,
				EnableTracking = true,
				Predicate = x => x.Id == createCategoryRequest.ParentCategoryId,
			};

			CategoryEntity? parentCategory = await this.categoryReadRepository.GetAsync(parameters);
			ArgumentNullException.ThrowIfNull(parentCategory, "Üst kategori bulunamadı!");
		}

		CategoryEntity category = this.mapper.Map<CategoryEntity>(createCategoryRequest);

		Task.WaitAll(new Task[2] {
			this.categoryWriteRepository.AddAsync(category, cancellationToken).AsTask(),
			this.categoryWriteRepository.SaveChangesAsync(cancellationToken)
		}, cancellationToken);
	}

	public async Task<IPaginate<GetCategoriesResponse>> GetCategoriesAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken) {
		GetPaginatedListParameters<CategoryEntity> parameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			PaginationOptions = new() {
				Index = paginationRequest.Page,
				Size = paginationRequest.Size,
			},
			OrderBy = x => x.OrderBy(x => x.Name),
		};
		IPaginate<CategoryEntity> categories = 
			await this.categoryReadRepository.GetPaginatedListAsync(parameters);
		IPaginate<GetCategoriesResponse> mappedCategories = 
			this.mapper.Map<Paginate<GetCategoriesResponse>>(categories);
		return mappedCategories;
	}

	public async Task<GetCategoryResponse> GetCategoryAsync(CategoryIdRequest categoryIdRequest, CancellationToken cancellationToken) {
		CategoryEntity? category = await this.categoryReadRepository.GetAsync(new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			Predicate = x => x.Id == categoryIdRequest.Value,
			Include = x => x.Include(x => x.ChildCategories)
		});

		ArgumentNullException.ThrowIfNull(category, "Kategori bulunamadı!");
		return this.mapper.Map<GetCategoryResponse>(category);
	}

	public async Task UpdateCategoryAsync(UpdateCategoryRequest updateCategoryRequest, CancellationToken cancellationToken) {
		CategoryEntity? category = await this.categoryReadRepository.GetAsync(new() {
			CancellationToken = cancellationToken,
			EnableTracking = true,
			Predicate = x => x.Id == updateCategoryRequest.Id,
		});
		ArgumentNullException.ThrowIfNull(category, "Kategori bulunamadı!");

		if(updateCategoryRequest.ParentCategoryId is not null) {
			GetParameters<CategoryEntity> parameters = new() {
				CancellationToken = cancellationToken,
				EnableTracking = true,
				Predicate = x => x.Id == updateCategoryRequest.ParentCategoryId,
			};

			CategoryEntity? parentCategory = await this.categoryReadRepository.GetAsync(parameters);
			ArgumentNullException.ThrowIfNull(parentCategory, "Üst kategori bulunamadı!");
			category.SetParentCategory(parentCategory);
		}

		CategoryEntity updatedCategory = this.mapper.Map(updateCategoryRequest, category);
		Task.WaitAll(new Task[2] {
			this.categoryWriteRepository.UpdateAsync(updatedCategory, cancellationToken).AsTask(),
			this.categoryWriteRepository.SaveChangesAsync(cancellationToken),
		}, cancellationToken);
	}

	public async Task DeleteCategoryAsync(DeleteCategoryRequest deleteCategoryRequest, CancellationToken cancellationToken) {
		GetParameters<CategoryEntity> parameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = true,
			Predicate = x => x.Id == deleteCategoryRequest.Id,
		};
		CategoryEntity? category = await this.categoryReadRepository.GetAsync(parameters);
		ArgumentNullException.ThrowIfNull(category, "Kategori bulunamadı!");

		Task.WaitAll(new Task[2] {
			this.categoryWriteRepository.DeleteAsync(category, cancellationToken).AsTask(),
			this.categoryWriteRepository.SaveChangesAsync(cancellationToken)
		}, cancellationToken);
	}

	public async Task<GetCategoryWithProductsResponse> GetCategoryWithProductsAsync(
		CategoryIdRequest categoryIdRequest,
		PaginationRequest paginationRequest,
		CancellationToken cancellationToken) {
		CategoryEntity? category = await this.categoryReadRepository.GetAsync(new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			Predicate = x => x.Id == categoryIdRequest.Value,
		});
		ArgumentNullException.ThrowIfNull(category, "Kategori bulunamadı!");
		IPaginate<ProductEntity> products = 
			await this.productReadRepository.GetProductsByCategoryId(
				categoryIdRequest.Value,
				paginationRequest,
				cancellationToken);

		GetCategoryWithProductsResponse mappedGetCategoryWithProductsRequest = 
			this.mapper.Map<GetCategoryWithProductsResponse>(category) with {
				Products = this.mapper.Map<Paginate<GetProductResponse>>(products)
			};
		return mappedGetCategoryWithProductsRequest;
	}
}