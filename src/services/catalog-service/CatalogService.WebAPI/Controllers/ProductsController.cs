using BuildingBlocks.Application.Pagination;
using CatalogService.Application.Dtos.Requests.Category;
using CatalogService.Application.Dtos.Requests.Product;
using CatalogService.Application.Features.Products.Commands.CreateProduct;
using CatalogService.Application.Features.Products.Commands.DeleteProduct;
using CatalogService.Application.Features.Products.Commands.UpdateProduct;
using CatalogService.Application.Features.Products.Queries.GetProductsByCategoryId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase {
	private readonly ISender sender;

	public ProductsController(ISender sender) {
		this.sender = sender;
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken) {
		await this.sender.Send(new CreateProductCommand() {
			CreateProductRequest = request
		}, cancellationToken);
		return this.Ok();
	}

	[HttpPut("{id.Value}")]
	public async Task<IActionResult> Update(
		[FromRoute] ProductIdRequest id,
		[FromBody] UpdateProductRequest request,
		CancellationToken cancellationToken) {
		request.Id = id.Value;
		await this.sender.Send(new UpdateProductCommand() {
			UpdateProductRequest = request
		}, cancellationToken);
		return this.Ok();
	}

	[HttpDelete("{request.id}")]
	public async Task<IActionResult> Delete([FromRoute] DeleteProductRequest request, CancellationToken cancellationToken) {
		await this.sender.Send(new DeleteProductCommand() {
			DeleteProductRequest = request
		}, cancellationToken);
		return this.Ok();
	}

	[HttpGet("{categoryId.Value}")]
	public async Task<IActionResult> GetByCategoryId(
		[FromRoute] CategoryIdRequest categoryId,
		[FromQuery] PaginationRequest paginationRequest,
		CancellationToken cancellationToken) {
		return this.Ok(await this.sender.Send(new GetProductsByCategoryIdQuery() {
			CategoryId = categoryId,
			PaginationRequest = paginationRequest
		}, cancellationToken));
	}
}