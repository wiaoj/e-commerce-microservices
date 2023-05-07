using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using CatalogService.Application.Features.Categories.Dtos;

namespace CatalogService.Application.Services;
public interface ICategoryService {
	public Task AddCategoryAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken);
	public Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken);
	public Task DeleteCategoryAsync(DeleteCategoryDto deleteCategoryDto, CancellationToken cancellationToken);
	public Task<IPaginate<GetCategoriesDto>> GetCategoriesAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken);
	public Task<GetCategoryDto> GetCategoryAsync(CategoryIdDto categoryIdDto, CancellationToken cancellationToken);
	public Task<GetCategoryWithProductsDto> GetCategoryWithProductsAsync(CategoryIdDto categoryIdDto, CancellationToken cancellationToken);
}