namespace CatalogService.Application.Dtos.Responses.Product;
public sealed record GetProductResponse(
	Guid Id,
	String Name,
	String? Description,
	Decimal Price,
	UInt16 Stock) : IResponseModel;