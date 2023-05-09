using System.Text.Json.Serialization;

namespace CatalogService.Application.Dtos.Requests.Product;
public sealed record UpdateProductRequest(
	String? Name,
	String? Description,
	Decimal? Price,
	UInt16? Stock,
	IEnumerable<Guid>? CategoryIds) : IRequestModel {
	[JsonIgnore]
	public Guid Id { get; set; }
};