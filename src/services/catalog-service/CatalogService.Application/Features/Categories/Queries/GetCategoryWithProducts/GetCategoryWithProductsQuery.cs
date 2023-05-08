using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Categories.Queries.GetCategoryWithProducts;
public sealed record GetCategoryWithProductsQuery : IRequest<GetCategoryWithProductsDto> {
	public required CategoryIdDto Id { get; set; }

	private class Handler : IRequestHandler<GetCategoryWithProductsQuery, GetCategoryWithProductsDto> {
		private readonly ICategoryService categoryService;

		public Handler(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public Task<GetCategoryWithProductsDto> Handle(GetCategoryWithProductsQuery request, CancellationToken cancellationToken) {
			return this.categoryService.GetCategoryWithProductsAsync(request.Id, cancellationToken);
		}
	}
}