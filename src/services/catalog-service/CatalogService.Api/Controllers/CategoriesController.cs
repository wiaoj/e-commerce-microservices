using BuildingBlocks.Application.Pagination;
using CatalogService.Application.Features.Categories.Commands.CreateCategory;
using CatalogService.Application.Features.Categories.Commands.DeleteCategory;
using CatalogService.Application.Features.Categories.Commands.UpdateCategor;
using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Application.Features.Categories.Queries.GetCategories;
using CatalogService.Application.Features.Categories.Queries.GetCategory;
using CatalogService.Application.Features.Categories.Queries.GetCategoryWithProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase {
	private readonly ISender sender;

	public CategoriesController(ISender sender) {
		this.sender = sender;
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateCategoryDto request, CancellationToken cancellationToken) {
		await this.sender.Send(new CreateCategoryCommand() {
			CreateCategory = request
		}, cancellationToken);
		return this.Ok();
	}

	[HttpPut("{id.Value}")]
	public async Task<IActionResult> Update([FromRoute] CategoryIdDto id, [FromBody] UpdateCategoryDto request, CancellationToken cancellationToken) {
		request.Id = id.Value;
		await this.sender.Send(new UpdateCategoryCommand() {
			UpdateCategory = request
		}, cancellationToken);
		return this.Ok();
	}

	[HttpDelete("{request.id}")]
	public async Task<IActionResult> Delete(DeleteCategoryDto request, CancellationToken cancellationToken) {
		await this.sender.Send(new DeleteCategoryCommand() {
			DeleteCategory = request
		}, cancellationToken);
		return this.Ok();
	}

	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request, CancellationToken cancellationToken) {
		return this.Ok(await this.sender.Send(new GetCategoriesQuery() {
			PaginationRequest = request
		}, cancellationToken));
	}

	[HttpGet("{id.Value}")]
	public async Task<IActionResult> GetById([FromRoute] CategoryIdDto id, CancellationToken cancellationToken) {
		return this.Ok(await this.sender.Send(new GetCategoryQuery() {
			Id = id
		}, cancellationToken));
	}

	[HttpGet("[action]/{id.Value}")]
	public async Task<IActionResult> GetByIdWithProducts([FromRoute] CategoryIdDto id, CancellationToken cancellationToken) {
		return this.Ok(await this.sender.Send(new GetCategoryWithProductsQuery() {
			Id = id
		}, cancellationToken));
	}
}