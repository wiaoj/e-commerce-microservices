using BuildingBlocks.Application.Pagination;
using CatalogService.Application.Dtos.Requests.Category;
using CatalogService.Application.Dtos.Responses.Category;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Categories.Queries.GetCategoryWithProducts;
public sealed record GetCategoryWithProductsQuery : IRequest<GetCategoryWithProductsResponse> {
	public required CategoryIdRequest Id { get; set; }
	public required PaginationRequest PaginationRequest { get; set; }

	private class Handler : IRequestHandler<GetCategoryWithProductsQuery, GetCategoryWithProductsResponse> {
		private readonly ICategoryService categoryService;

		public Handler(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public Task<GetCategoryWithProductsResponse> Handle(GetCategoryWithProductsQuery request, CancellationToken cancellationToken) {
			return this.categoryService.GetCategoryWithProductsAsync(request.Id, request.PaginationRequest, cancellationToken);
		}
	}
}