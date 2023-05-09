using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using CatalogService.Application.Features.Products.Dtos;

namespace CatalogService.Application.Features.Categories.Dtos;
public sealed record GetCategoryWithProductsDto {
	public Guid Id { get; set; }
	public String Name { get; set; } = null!;
	public IPaginate<GetProductDto>? Products { get; set; }
}