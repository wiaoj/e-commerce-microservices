namespace CatalogService.Application.Dtos.Requests.Category;
public sealed record DeleteCategoryRequest(Guid Id) : IRequestModel;