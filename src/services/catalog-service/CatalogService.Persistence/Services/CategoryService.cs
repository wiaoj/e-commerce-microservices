using BuildingBlocks.Persistence.EFCore.Parameters;
using CatalogService.Application.Features.Categories.Commands.CreateCategory;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Persistence.Repositories.Interfaces;

namespace CatalogService.Persistence.Services;
internal sealed class CategoryService : ICategoryService {
	private readonly ICategoryWriteRepository categoryWriteRepository;
	private readonly ICategoryReadRepository categoryReadRepository;

	public CategoryService(ICategoryWriteRepository categoryWriteRepository,
						ICategoryReadRepository categoryReadRepository) {
		this.categoryWriteRepository = categoryWriteRepository;
		this.categoryReadRepository = categoryReadRepository;
	}

	public async Task AddCategoryAsync(CreateCategoryCommand command, CancellationToken cancellationToken) {
		CategoryEntity category;
		if(command.ParentCategoryId is null) {
			category = new(command.Name);
		} else {
			GetParameters<CategoryEntity> parameters = new() {
				CancellationToken = cancellationToken,
				EnableTracking = true,
				Predicate = x => x.Id == command.ParentCategoryId,
			};

			CategoryEntity? parentCategory = await this.categoryReadRepository.GetAsync(parameters);
			ArgumentNullException.ThrowIfNull(parentCategory, "Üst kategori yanlış!");

			category = new(command.Name, parentCategory);

		}

		await this.categoryWriteRepository.AddAsync(category, cancellationToken);
	}
}