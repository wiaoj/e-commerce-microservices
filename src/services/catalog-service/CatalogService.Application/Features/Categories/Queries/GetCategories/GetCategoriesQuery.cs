using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using CatalogService.Application.Dtos.Responses.Category;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Categories.Queries.GetCategories;
public sealed record GetCategoriesQuery : IRequest<IPaginate<GetCategoriesResponse>> {
	public required PaginationRequest PaginationRequest { get; set; }

	private class Handler : IRequestHandler<GetCategoriesQuery, IPaginate<GetCategoriesResponse>> {
		private readonly ICategoryService categoryService;

		public Handler(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public Task<IPaginate<GetCategoriesResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken) {
			return this.categoryService.GetCategoriesAsync(request.PaginationRequest, cancellationToken);
		}
	}
}