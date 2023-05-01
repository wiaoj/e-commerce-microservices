using CatalogService.Application.Features.Categories.Commands.CreateCategory;

namespace CatalogService.Application.Services;
public interface ICategoryService {
	public Task AddCategoryAsync(Command command, CancellationToken cancellationToken);
}