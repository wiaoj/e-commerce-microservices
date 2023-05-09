namespace CatalogService.Application.Dtos.Requests.Product;
public sealed record DeleteProductRequest(Guid Id) : IRequestModel;