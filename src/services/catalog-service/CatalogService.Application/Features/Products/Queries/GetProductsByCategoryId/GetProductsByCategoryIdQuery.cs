using BuildingBlocks.Application.Abstraction.Pagination;
using CatalogService.Application.Features.Products.Dtos;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Products.Queries.GetProductsByCategoryId;
public sealed record GetProductsByCategoryIdQuery : IRequest<IPaginate<GetProductDto>> {
	public required Guid CategoryId { get; set; }

	private sealed class Handler : IRequestHandler<GetProductsByCategoryIdQuery, IPaginate<GetProductDto>> {
		private readonly IProductService productService;

		public Handler(IProductService productService) {
			this.productService = productService;
		}

		public async Task<IPaginate<GetProductDto>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken) {
			return await this.productService.GetProductsByCategoryId(request.CategoryId, cancellationToken);
		}
	}
}