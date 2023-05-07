using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Categories.Queries.GetCategory;
public sealed record GetCategoryQuery : IRequest<GetCategoryDto> {
	public required Guid Id { get; set; }

	private class Handler : IRequestHandler<GetCategoryQuery, GetCategoryDto> {
		private readonly ICategoryService categoryService;

		public Handler(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public Task<GetCategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken) {
			return this.categoryService.GetCategoryAsync(request, cancellationToken);
		}
	}
}