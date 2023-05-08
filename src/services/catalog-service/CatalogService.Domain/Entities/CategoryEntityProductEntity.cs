namespace CatalogService.Domain.Entities;
public sealed class CategoryEntityProductEntity {
	public Guid CategoryId { get; set; }
	public CategoryEntity? Category { get; set; }
	public Guid ProductId { get; set; }
	public ProductEntity? Product { get; set; }

	//public CategoryEntityProductEntity(Guid categoryId, Guid productId) {
	//	this.CategoryId = categoryId;
	//	this.ProductId = productId;
	//}

	//public CategoryEntityProductEntity(CategoryEntity category, ProductEntity product) {
	//	this.SetCategory(category).SetProduct(product);
	//}

	//private CategoryEntityProductEntity SetCategory(CategoryEntity category) {
	//	this.CategoryId = category.Id;
	//	this.Category = category;
	//	return this;
	//}

	//private CategoryEntityProductEntity SetProduct(ProductEntity product) {
	//	this.ProductId = product.Id;
	//	this.Product = product;
	//	return this;
	//}
}