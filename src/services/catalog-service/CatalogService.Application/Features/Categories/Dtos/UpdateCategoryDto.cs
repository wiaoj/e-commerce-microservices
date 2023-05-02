namespace CatalogService.Application.Features.Categories.Dtos;
public sealed record UpdateCategoryDto {
	public required Guid Id { get; set; }
	public String Name { get; set; }
	public Guid? ParentCategoryId { get; set; }
}