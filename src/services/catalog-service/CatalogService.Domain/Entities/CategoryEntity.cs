using BuildingBlocks.Domain;

namespace CatalogService.Domain.Entities;
public sealed class CategoryEntity : Entity {
	public String Name { get; set; }
	public Guid? ParentCategoryId { get; set; }
	public CategoryEntity? ParentCategory { get; set; }
	public ICollection<CategoryEntity> ChildCategories { get; set; }

	public CategoryEntity(String name) {
		ArgumentException.ThrowIfNullOrEmpty(name, "Kategori ismi boş olamaz!");
		this.Name = name;
		this.ChildCategories = new List<CategoryEntity>();
	}

	public CategoryEntity(String name, CategoryEntity parentCategory) : this(name) {
		ArgumentNullException.ThrowIfNull(parentCategory, "Üst kategori bulunamadı!");
		SetParentCategory(parentCategory);
	}

	public CategoryEntity SetParentCategory(CategoryEntity parentCategory) {
		this.ParentCategory = parentCategory;
		this.ParentCategoryId = parentCategory.Id;
		return this;
	}
}