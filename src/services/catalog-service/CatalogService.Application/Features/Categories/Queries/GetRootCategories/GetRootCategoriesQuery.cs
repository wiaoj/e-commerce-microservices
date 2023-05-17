using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using CatalogService.Application.Dtos.Responses.Category;
using CatalogService.Application.Features.Categories.Queries.GetCategories;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Categories.Queries.GetRootCategories;
public sealed record GetRootCategoriesQuery : IRequest<IEnumerable<GetRootCategoriesResponse>> {

	private class Handler : IRequestHandler<GetRootCategoriesQuery, IEnumerable<GetRootCategoriesResponse>> {
		private readonly ICategoryService categoryService;

		public Handler(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public Task<IEnumerable<GetRootCategoriesResponse>> Handle(GetRootCategoriesQuery request, CancellationToken cancellationToken) {
			return this.categoryService.GetRootCategoriesResponseAsync(cancellationToken);
		}
	}
}