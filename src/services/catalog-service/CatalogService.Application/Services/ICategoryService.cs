using CatalogService.Application.Features.Categories.Commands.CreateCategory;

namespace CatalogService.Application.Services;
public interface ICategoryService {
	public Task AddCategoryAsync(CreateCategoryCommand command, CancellationToken cancellationToken);
}