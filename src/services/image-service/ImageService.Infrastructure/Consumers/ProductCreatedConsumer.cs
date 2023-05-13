using ImageService.Application.Services;
using ImageService.Domain.Entities;
using MassTransit;
using MessagingContracts.Catalog;
using Microsoft.Extensions.Logging;

namespace ImageService.Infrastructure.Consumers;
public sealed class ProductCreatedConsumer : IConsumer<ProductCreated> {
	private readonly IProductService productService;

	public ProductCreatedConsumer(IProductService productService) {
		this.productService = productService;
	}

	public async Task Consume(ConsumeContext<ProductCreated> context) {
		ProductEntity product = new() {
			Id = context.Message.Id
		};
		await this.productService.AddProduct(product, context.CancellationToken);
	}
}