using CatalogService.Application.Dtos.Requests.Category;
using CatalogService.Application.Dtos.Responses.Category;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Categories.Queries.GetCategory;
public sealed record GetCategoryQuery : IRequest<GetCategoryResponse> {
	public required CategoryIdRequest CategoryIdRequest { get; set; }

	private class Handler : IRequestHandler<GetCategoryQuery, GetCategoryResponse> {
		private readonly ICategoryService categoryService;

		public Handler(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public Task<GetCategoryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken) {
			return this.categoryService.GetCategoryAsync(request.CategoryIdRequest, cancellationToken);
		}
	}
}