using BuildingBlocks.Domain;

namespace ImageService.Domain.Entities;
public sealed class ProductImageEntity : Entity {
	public String Name { get; set; }
	public String Path { get; set; }
	public Guid ProductId { get; set; }
	public Boolean Showcase { get; set; }

	public ProductEntity? ProductEntity { get; set; }
}