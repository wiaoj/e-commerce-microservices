using System.Text.Json.Serialization;

namespace CatalogService.Application.Dtos.Requests.Category;
public sealed record UpdateCategoryRequest(String? Name, Guid? ParentCategoryId) : IRequestModel {
	[JsonIgnore]
	public Guid Id { get; set; }
}