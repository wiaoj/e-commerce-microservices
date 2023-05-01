using BuildingBlocks.Domain;

namespace CatalogService.Domain.Entities;
public sealed class CategoryEntity : Entity {
	public required String Name { get; set; }
	public Guid? ParentCategoryId { get; set; }
	public CategoryEntity? ParentCategory { get; set; }

	public CategoryEntity(String name) {
		ArgumentException.ThrowIfNullOrEmpty(name, "Kategori ismi boş olamaz!");
		this.Name = name;
	}

	public CategoryEntity(String name, CategoryEntity parentCategory) : this(name) {
		ArgumentNullException.ThrowIfNull(parentCategory, "Üst kategori bulunamadı!");
		this.ParentCategory = parentCategory;
		this.ParentCategoryId = parentCategory.Id;
	}
}