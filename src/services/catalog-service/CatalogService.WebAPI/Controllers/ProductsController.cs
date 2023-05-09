using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Application.Features.Products.Commands.CreateProduct;
using CatalogService.Application.Features.Products.Commands.DeleteProduct;
using CatalogService.Application.Features.Products.Commands.UpdateProduct;
using CatalogService.Application.Features.Products.Dtos;
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
	public async Task<IActionResult> Create(CreateProductDto request, CancellationToken cancellationToken) {
		await this.sender.Send(new CreateProductCommand() {
			CreateProduct = request
		}, cancellationToken);
		return this.Ok();
	}

	[HttpPut("{id.Value}")]
	public async Task<IActionResult> Update([FromRoute] CategoryIdDto id, [FromBody] UpdateProductDto request, CancellationToken cancellationToken) {
		request.Id = id.Value;
		await this.sender.Send(new UpdateProductCommand() {
			UpdateProduct = request
		}, cancellationToken);
		return this.Ok();
	}

	[HttpDelete("{request.id}")]
	public async Task<IActionResult> Delete([FromRoute] DeleteProductDto request, CancellationToken cancellationToken) {
		await this.sender.Send(new DeleteProductCommand() {
			DeleteProduct = request
		}, cancellationToken);
		return this.Ok();
	}

	[HttpGet("{request.categoryId}")]
	public async Task<IActionResult> Delete([FromRoute] GetProductsByCategoryIdQuery request, CancellationToken cancellationToken) {
		return this.Ok(await this.sender.Send(request, cancellationToken));
	}
}