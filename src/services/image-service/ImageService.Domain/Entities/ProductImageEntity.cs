namespace ImageService.Domain.Entities;
public sealed class ProductImageEntity : ImageEntity {
	public Guid ProductId { get; set; }
	public Boolean Showcase { get; set; }
	public ProductEntity? Product { get; set; }
}