using CatalogService.Domain.Entities;

namespace CatalogService.Application.Features.Products.Dtos;
public sealed record UpdateProductDto {
	public Guid Id { get; set; }
	public String? Name { get; set; }
	public String? Description { get; set; }

	public Decimal? Price { get; set; }
	public UInt16? Stock { get; set; }

	public ICollection<CategoryEntity>? Categories { get; set; }
}