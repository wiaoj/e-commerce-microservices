using BuildingBlocks.Application.Abstraction.Pagination;
using CatalogService.Application.Dtos.Responses.Product;

namespace CatalogService.Application.Dtos.Responses.Category;
public sealed record GetCategoryWithProductsResponse(
	Guid Id,
	String Name) : IResponseModel {
	public IPaginate<GetProductResponse>? Products { get; set; }
}