using BuildingBlocks.Domain;

namespace ImageService.Domain.Entities;
public abstract class ImageEntity : Entity {
	public String Name { get; set; }
	public String Path { get; set; }
}