namespace CatalogService.Application.Dtos.Requests.Product;
public sealed record CreateProductRequest(
	String Name,
	String? Description,
	Decimal Price,
	UInt16 Stock,
	IEnumerable<Guid>? CategoryIds) : IRequestModel;