using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Categories.Commands.CreateCategory;
public sealed record CreateCategoryCommand : IRequest<Unit> {
	public required CreateCategoryDto CreateCategory { get; set; }

	private sealed class Handler : IRequestHandler<CreateCategoryCommand, Unit> {
		private readonly ICategoryService categoryService;

		public Handler(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken) {
			await this.categoryService.AddCategoryAsync(request.CreateCategory, cancellationToken);

			return Unit.Value;
		}
	}
}