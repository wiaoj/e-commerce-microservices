using BuildingBlocks.Domain;

namespace CatalogService.Domain.Entities;
public sealed class ProductVariantEntity : Entity {
	public Guid ProductId { get; set; }
	public ProductEntity? Product { get; set; }

	public Decimal Price { get; set; }
	public UInt16 Stock { get; set; }

	public ProductVariantEntity(Guid productId, Decimal price, UInt16 stock) {
		this.ProductId = productId;
		this.Price = price;
		this.Stock = stock;
	}
}