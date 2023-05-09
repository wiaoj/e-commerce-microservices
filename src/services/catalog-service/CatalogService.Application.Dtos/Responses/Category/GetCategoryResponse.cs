namespace CatalogService.Application.Dtos.Responses.Category;
public sealed record GetCategoryResponse(
	Guid Id,
	String Name,
	Guid? ParentCategoryId,
	List<GetCategoryResponse> ChildCategories) : IResponseModel;