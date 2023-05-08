namespace CatalogService.Application.Features.Products.Dtos;
public sealed record CreateProductDto {
	public required String Name { get; init; }
	public String? Description { get; init; }

	public required Decimal Price { get; init; }
	public required UInt16 Stock { get; init; }

	public IEnumerable<Guid>? CategoryIds { get; init; }
}