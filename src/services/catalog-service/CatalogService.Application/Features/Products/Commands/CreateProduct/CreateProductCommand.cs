using CatalogService.Application.Features.Products.Dtos;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Products.Commands.CreateProduct;
public sealed record CreateProductCommand : IRequest<Unit> {
	public required CreateProductDto CreateProduct { get; set; }

	private sealed class Handler : IRequestHandler<CreateProductCommand, Unit> {
		private readonly IProductService productService;

		public Handler(IProductService productService) {
			this.productService = productService;
		}

		public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken) {
			await this.productService.AddProduct(request.CreateProduct, cancellationToken);

			return Unit.Value;
		}
	}
}