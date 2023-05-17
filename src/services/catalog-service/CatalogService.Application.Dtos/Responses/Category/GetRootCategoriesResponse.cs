namespace CatalogService.Application.Dtos.Responses.Category;
public sealed record GetRootCategoriesResponse(Guid Id, String Name) : IRequestModel;