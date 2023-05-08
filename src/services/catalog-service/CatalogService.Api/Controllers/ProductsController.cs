using CatalogService.Application.Features.Products.Commands.CreateProduct;
using CatalogService.Application.Features.Products.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Api.Controllers;
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
}