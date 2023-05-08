using System.Text.Json.Serialization;

namespace CatalogService.Application.Features.Categories.Dtos;
public sealed record UpdateCategoryDto {
	[JsonIgnore]
	public Guid Id { get; set; }
	public String Name { get; set; }
	public Guid? ParentCategoryId { get; set; }
}