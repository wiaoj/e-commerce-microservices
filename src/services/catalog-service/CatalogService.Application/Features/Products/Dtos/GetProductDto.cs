namespace CatalogService.Application.Features.Products.Dtos;
public sealed record GetProductDto(Guid Id, String Name, String? Description, Decimal Price, UInt16 Stock);