using BuildingBlocks.Application.Abstraction.Pagination;
using CatalogService.Application.Features.Categories.Commands.CreateCategory;
using CatalogService.Application.Features.Categories.Commands.UpdateCategorCommand;
using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Application.Features.Categories.Queries.GetCategories;
using CatalogService.Application.Features.Categories.Queries.GetCategory;

namespace CatalogService.Application.Services;
public interface ICategoryService {
	public Task AddCategoryAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken);
	public Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken);
	public Task DeleteCategoryAsync(DeleteCategoryDto deleteCategoryDto, CancellationToken cancellationToken);
	public Task<IPaginate<GetCategoriesDto>> GetCategoriesAsync(GetCategoriesQuery query, CancellationToken cancellationToken);
	public Task<GetCategoryDto> GetCategoryAsync(GetCategoryQuery query, CancellationToken cancellationToken);
}