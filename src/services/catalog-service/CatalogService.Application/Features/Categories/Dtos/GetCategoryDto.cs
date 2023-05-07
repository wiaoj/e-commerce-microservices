namespace CatalogService.Application.Features.Categories.Dtos;
public sealed record GetCategoryDto {
	public Guid Id { get; set; }
	public String Name { get; set; } = null!;
	public Guid ParentCategoryId { get; set; } = Guid.Empty;
	public List<GetCategoryDto> ChildCategories { get; set; } = new();
}