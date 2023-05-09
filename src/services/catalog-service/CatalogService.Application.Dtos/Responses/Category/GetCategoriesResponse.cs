namespace CatalogService.Application.Dtos.Responses.Category;
public record GetCategoriesResponse(
	Guid Id,
	String Name,
	Guid? ParentCategoryId) : IResponseModel;