using BuildingBlocks.Domain;

namespace CatalogService.Domain.Entities;
public sealed class ProductEntity : Entity {
	public String Name { get; set; }
	public String? Description { get; set; }

	public Decimal Price { get; set; }
	public UInt16 Stock { get; set; }

	public ICollection<CategoryEntity> Categories { get; private set; }

	public ProductEntity(String name, Decimal price, UInt16 stock) : this(name, String.Empty, price, stock) { }
	public ProductEntity(String name, String description, Decimal price, UInt16 stock) {
		this.Name = name;
		this.Description = description;
		this.Price = price;
		this.Stock = stock;
		this.Categories = new HashSet<CategoryEntity>();
	}

	public ProductEntity AddCategory(CategoryEntity category) {
		this.Categories.Add(category);
		return this;
	}

	public ProductEntity AddRangeCategories(IEnumerable<CategoryEntity> categories) {
		categories.ToList().ForEach(this.Categories.Add);
		return this;
	}
}