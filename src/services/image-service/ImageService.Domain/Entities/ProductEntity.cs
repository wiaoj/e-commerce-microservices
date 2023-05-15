using BuildingBlocks.Domain;

namespace ImageService.Domain.Entities;
public sealed class ProductEntity : Entity {
	public ICollection<ProductImageEntity> ProductImages { get; set; }
}