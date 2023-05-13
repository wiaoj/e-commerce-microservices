using ImageService.Application.Services;
using ImageService.Application.Storage;
using MassTransit;
using MessagingContracts.Catalog;

namespace ImageService.Infrastructure.Consumers;
public sealed class ProductDeletedConsumer : IConsumer<ProductDeleted> {
	private readonly IProductService productService;

	public ProductDeletedConsumer(IProductService productService) {
		this.productService = productService;
	}

	public async Task Consume(ConsumeContext<ProductDeleted> context) {
		await this.productService.DeleteProduct(context.Message.Id, context.CancellationToken);
	}
}