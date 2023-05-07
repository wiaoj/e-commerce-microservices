using BuildingBlocks.Domain;

namespace CatalogService.Domain.Entities;
public sealed class ProductEntity : Entity {
	public String Name { get; set; }
	public String? Description { get; set; }

	public Decimal Price { get; set; }
	public UInt16 Stock { get; set; }

	public ICollection<CategoryEntity> Categories { get; set; }

	public ProductEntity(String name, Decimal price, UInt16 stock) : this(name, String.Empty, price, stock) { }
	public ProductEntity(String name, String description, Decimal price, UInt16 stock) {
		this.Name = name;
		this.Description = description;
		this.Price = price;
		this.Stock = stock;
		this.Categories = new HashSet<CategoryEntity>();
	}
}