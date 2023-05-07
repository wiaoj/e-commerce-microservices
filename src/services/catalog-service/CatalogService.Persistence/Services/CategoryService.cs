using AutoMapper;
using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Persistence.EFCore.Parameters;
using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Application.Features.Categories.Queries.GetCategories;
using CatalogService.Application.Features.Categories.Queries.GetCategory;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Persistence.Services;
internal sealed class CategoryService : ICategoryService {
	private readonly ICategoryWriteRepository categoryWriteRepository;
	private readonly ICategoryReadRepository categoryReadRepository;
	private readonly IMapper mapper;

	public CategoryService(ICategoryWriteRepository categoryWriteRepository,
						ICategoryReadRepository categoryReadRepository,
						IMapper mapper) {
		this.categoryWriteRepository = categoryWriteRepository;
		this.categoryReadRepository = categoryReadRepository;
		this.mapper = mapper;
	}

	public async Task AddCategoryAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken) {
		if(createCategoryDto.ParentCategoryId is not null) {
			GetParameters<CategoryEntity> parameters = new() {
				CancellationToken = cancellationToken,
				EnableTracking = true,
				Predicate = x => x.Id == createCategoryDto.ParentCategoryId,
			};

			CategoryEntity? parentCategory = await this.categoryReadRepository.GetAsync(parameters);
			ArgumentNullException.ThrowIfNull(parentCategory, "Üst kategori bulunamadı!");
		}

		CategoryEntity category = this.mapper.Map<CategoryEntity>(createCategoryDto);

		await this.categoryWriteRepository.AddAsync(category, cancellationToken);
		await this.categoryWriteRepository.SaveChangesAsync(cancellationToken);
	}

	public async Task<IPaginate<GetCategoriesDto>> GetCategoriesAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken) {
		GetListParameters<CategoryEntity> parameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			PaginationOptions = new() {
				Index = paginationRequest.Page,
				Size = paginationRequest.Size,
			},
			OrderBy = x => x.OrderBy(x => x.Name),
		};
		IPaginate<CategoryEntity> categories = await this.categoryReadRepository.GetListAsync(parameters);
		IPaginate<GetCategoriesDto> mappedCategories = this.mapper.Map<Paginate<GetCategoriesDto>>(categories);
		return mappedCategories;
	}

	public async Task<GetCategoryDto> GetCategoryAsync(GetCategoryQuery query, CancellationToken cancellationToken) {
		CategoryEntity? category = await this.categoryReadRepository.GetAsync(new() {
			CancellationToken = cancellationToken,
			EnableTracking = false,
			Predicate = x => x.Id == query.Id,
			Include = x => x.Include(x => x.ChildCategories)
		});

		ArgumentNullException.ThrowIfNull(category, "Kategori bulunamadı!");
		return this.mapper.Map<GetCategoryDto>(category);
	}

	public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken) {
		CategoryEntity? category = await this.categoryReadRepository.GetAsync(new() {
			CancellationToken = cancellationToken,
			EnableTracking = true,
			Predicate = x => x.Id == updateCategoryDto.Id,
		});
		ArgumentNullException.ThrowIfNull(category, "Kategori bulunamadı!");

		if(updateCategoryDto.ParentCategoryId is not null) {
			GetParameters<CategoryEntity> parameters = new() {
				CancellationToken = cancellationToken,
				EnableTracking = true,
				Predicate = x => x.Id == updateCategoryDto.ParentCategoryId,
			};

			CategoryEntity? parentCategory = await this.categoryReadRepository.GetAsync(parameters);
			ArgumentNullException.ThrowIfNull(parentCategory, "Üst kategori bulunamadı!");
			category.SetParentCategory(parentCategory);
		}

		CategoryEntity updatedCategory = this.mapper.Map(updateCategoryDto, category);
		Task.WaitAll(new Task[2] {
			this.categoryWriteRepository.UpdateAsync(updatedCategory, cancellationToken).AsTask(),
			this.categoryWriteRepository.SaveChangesAsync(cancellationToken),
		}, cancellationToken);
	}

	public async Task DeleteCategoryAsync(DeleteCategoryDto deleteCategoryDto, CancellationToken cancellationToken) {
		GetParameters<CategoryEntity> parameters = new() {
			CancellationToken = cancellationToken,
			EnableTracking = true,
			Predicate = x => x.Id == deleteCategoryDto.Id,
		};
		CategoryEntity? category = await this.categoryReadRepository.GetAsync(parameters);
		ArgumentNullException.ThrowIfNull(category, "Kategori bulunamadı!");

		Task.WaitAll(new Task[2] {
			this.categoryWriteRepository.DeleteAsync(category, cancellationToken).AsTask(),
			this.categoryWriteRepository.SaveChangesAsync(cancellationToken)
		}, cancellationToken);
	}
}