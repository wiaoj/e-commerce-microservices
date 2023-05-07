namespace CatalogService.Application.Features.Categories.Dtos;
public record GetCategoriesDto(Guid Id, String Name, Guid ParentCategoryId);