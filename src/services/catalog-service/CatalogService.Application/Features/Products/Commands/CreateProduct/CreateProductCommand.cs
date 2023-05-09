using CatalogService.Application.Dtos.Requests.Product;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Products.Commands.CreateProduct;
public sealed record CreateProductCommand : IRequest<Unit> {
	public required CreateProductRequest CreateProductRequest { get; set; }

	private sealed class Handler : IRequestHandler<CreateProductCommand, Unit> {
		private readonly IProductService productService;

		public Handler(IProductService productService) {
			this.productService = productService;
		}

		public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken) {
			await this.productService.AddProductAsync(request.CreateProductRequest, cancellationToken);

			return Unit.Value;
		}
	}
}