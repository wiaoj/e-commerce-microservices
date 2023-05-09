namespace CatalogService.Application.Dtos.Requests.Category;
public sealed record CreateCategoryRequest(
	String Name,
	Guid? ParentCategoryId) : IRequestModel;