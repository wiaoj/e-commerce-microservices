namespace CatalogService.Application.Features.Categories.Dtos;
public sealed record CreateCategoryDto {
	public required String Name { get; set; }
	public Guid? ParentCategoryId { get; set; }
}