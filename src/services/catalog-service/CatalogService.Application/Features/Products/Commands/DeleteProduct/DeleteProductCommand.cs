using CatalogService.Application.Dtos.Requests.Product;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Products.Commands.DeleteProduct;
public sealed record DeleteProductCommand : IRequest<Unit> {
	public required DeleteProductRequest DeleteProductRequest { get; set; }

	private sealed class Handler : IRequestHandler<DeleteProductCommand, Unit> {
		private readonly IProductService productService;

		public Handler(IProductService productService) {
			this.productService = productService;
		}

		public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken) {
			await this.productService.DeleteProductAsync(request.DeleteProductRequest, cancellationToken);

			return Unit.Value;
		}
	}
}