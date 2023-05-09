namespace CatalogService.Application.Dtos.Requests.Product;
public sealed record UpdateProductRequest(
	String? Name,
	String? Description,
	Decimal? Price,
	UInt16? Stock,
	IEnumerable<Guid>? CategoryIds) : IRequestModel {
	public Guid Id { get; set; }
};