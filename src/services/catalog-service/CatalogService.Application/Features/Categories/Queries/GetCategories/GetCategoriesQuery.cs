using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Categories.Queries.GetCategories;
public sealed record GetCategoriesQuery : IRequest<IPaginate<GetCategoriesDto>> {
	public required PaginationRequest PaginationRequest { get; set; }

	private class Handler : IRequestHandler<GetCategoriesQuery, IPaginate<GetCategoriesDto>> {
		private readonly ICategoryService categoryService;

		public Handler(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public Task<IPaginate<GetCategoriesDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken) {
			return this.categoryService.GetCategoriesAsync(request.PaginationRequest, cancellationToken);
		}
	}
}