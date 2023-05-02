namespace CatalogService.Contracts;
public sealed record UpdateCategory(String Name, Guid? ParentCategoryId);
public sealed record CategoryId(Guid Value);