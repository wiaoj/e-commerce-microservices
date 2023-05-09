using CatalogService.Application.Dtos.Requests.Product;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Products.Commands.UpdateProduct;
public sealed record UpdateProductCommand : IRequest<Unit> {
	public required UpdateProductRequest UpdateProductRequest { get; set; }

	private sealed class Handler : IRequestHandler<UpdateProductCommand, Unit> {
		private readonly IProductService productService;

		public Handler(IProductService productService) {
			this.productService = productService;
		}

		public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
			await this.productService.UpdateProductAsync(request.UpdateProductRequest, cancellationToken);

			return Unit.Value;
		}
	}
}
