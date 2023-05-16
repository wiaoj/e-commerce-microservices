using BuildingBlocks.Domain;

namespace CatalogService.Domain.Entities;
public sealed class VariantTypeOptionEntity : Entity {
	public String Value { get; set; }
	public Guid VariantTypeId { get; set; }
	public VariantTypeEntity? VariantType { get; set; }


	public VariantTypeOptionEntity(String value, Guid variantTypeId) {
		this.Value = value;
		this.VariantTypeId = variantTypeId;
	}
}