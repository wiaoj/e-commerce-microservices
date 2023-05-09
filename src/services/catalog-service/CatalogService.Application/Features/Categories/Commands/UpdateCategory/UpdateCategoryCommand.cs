using CatalogService.Application.Dtos.Requests.Category;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Categories.Commands.UpdateCategory;
public sealed record UpdateCategoryCommand : IRequest<Unit> {
	public required UpdateCategoryRequest UpdateCategoryRequest { get; set; }

	private sealed class Handler : IRequestHandler<UpdateCategoryCommand, Unit> {
		private readonly ICategoryService categoryService;

		public Handler(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken) {
			await this.categoryService.UpdateCategoryAsync(request.UpdateCategoryRequest, cancellationToken);

			return Unit.Value;
		}
	}
}