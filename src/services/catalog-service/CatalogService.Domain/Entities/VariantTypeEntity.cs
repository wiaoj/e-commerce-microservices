using BuildingBlocks.Domain;

namespace CatalogService.Domain.Entities;
public sealed class VariantTypeEntity : Entity {
	public String Name { get; set; }
	public ICollection<VariantTypeOptionEntity> VariantTypeOptions { get; set; }

	public VariantTypeEntity(String name) {
		this.Name = name;
		this.VariantTypeOptions = new HashSet<VariantTypeOptionEntity>();
	}
}