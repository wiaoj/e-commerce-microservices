using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Categories.Commands.DeleteCategory;
public sealed record DeleteCategoryCommand : IRequest<Unit> {
	public required DeleteCategoryDto DeleteCategory { get; set; }

	private sealed class Handler : IRequestHandler<DeleteCategoryCommand, Unit> {
		private readonly ICategoryService categoryService;

		public Handler(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) {
			await this.categoryService.DeleteCategoryAsync(request.DeleteCategory, cancellationToken);

			return Unit.Value;
		}
	}
}