using CatalogService.Application.Features.Products.Dtos;

namespace CatalogService.Application.Features.Categories.Dtos;
public sealed record GetCategoryWithProductsDto {
	public Guid Id { get; set; }
	public String Name { get; set; } = null!;
	public List<GetProductDto> Products { get; set; } = new();
}