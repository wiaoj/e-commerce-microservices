using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using CatalogService.Application.Dtos.Requests.Category;
using CatalogService.Application.Dtos.Responses.Product;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Products.Queries.GetProductsByCategoryId;
public sealed record GetProductsByCategoryIdQuery : IRequest<IPaginate<GetProductResponse>> {
	public required CategoryIdRequest CategoryId { get; set; }
	public required PaginationRequest paginationRequest { get; set; }

	private sealed class Handler : IRequestHandler<GetProductsByCategoryIdQuery, IPaginate<GetProductResponse>> {
		private readonly IProductService productService;

		public Handler(IProductService productService) {
			this.productService = productService;
		}

		public async Task<IPaginate<GetProductResponse>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken) {
			return await this.productService.GetProductsByCategoryId(request.CategoryId, request.paginationRequest, cancellationToken);
		}
	}
}