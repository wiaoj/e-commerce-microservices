using CatalogService.Application.Features.Products.Dtos;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Products.Commands.DeleteProduct;
public sealed record DeleteProductCommand : IRequest<Unit> {
	public required DeleteProductDto DeleteProduct { get; set; }

	private sealed class Handler : IRequestHandler<DeleteProductCommand, Unit> {
		private readonly IProductService productService;

		public Handler(IProductService productService) {
			this.productService = productService;
		}

		public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken) {
			await this.productService.DeleteProductAsync(request.DeleteProduct, cancellationToken);

			return Unit.Value;
		}
	}
}