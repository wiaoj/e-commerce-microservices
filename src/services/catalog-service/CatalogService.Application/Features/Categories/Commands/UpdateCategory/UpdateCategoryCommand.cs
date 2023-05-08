using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Categories.Commands.UpdateCategory;
public sealed record UpdateCategoryCommand : IRequest<Unit> {
	public required UpdateCategoryDto UpdateCategory { get; set; }

	private sealed class Handler : IRequestHandler<UpdateCategoryCommand, Unit> {
		private readonly ICategoryService categoryService;

		public Handler(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken) {
			await this.categoryService.UpdateCategoryAsync(request.UpdateCategory, cancellationToken);

			return Unit.Value;
		}
	}
}