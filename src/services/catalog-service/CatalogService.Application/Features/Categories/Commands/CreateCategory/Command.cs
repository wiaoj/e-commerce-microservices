using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Categories.Commands.CreateCategory;
public sealed record Command : IRequest<Unit> {
	public required String Name { get; set; }
	public Guid? ParentCategoryId { get; set; }

	private class Handler : IRequestHandler<Command, Unit> {
		private readonly ICategoryService categoryService;

		public Handler(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public async Task<Unit> Handle(Command request, CancellationToken cancellationToken) {
			await this.categoryService.AddCategoryAsync(request, cancellationToken);

			return Unit.Value;
		}
	}
}