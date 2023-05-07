using BuildingBlocks.Application.Pagination;
using CatalogService.Application.Features.Categories.Commands.CreateCategory;
using CatalogService.Application.Features.Categories.Commands.DeleteCategoryCommand;
using CatalogService.Application.Features.Categories.Commands.UpdateCategorCommand;
using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Application.Features.Categories.Queries.GetCategories;
using CatalogService.Application.Features.Categories.Queries.GetCategory;
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

	[HttpPut("{id}")]
	public async Task<IActionResult> Update([FromBody] UpdateCategoryDto request, CancellationToken cancellationToken) {
		await this.sender.Send(new UpdateCategoryCommand() {
			UpdateCategory = request
		}, cancellationToken);
		return this.Ok();
	}

	[HttpDelete("{request.id}")]
	public async Task<IActionResult> Delete([FromRoute] DeleteCategoryDto request, CancellationToken cancellationToken) {
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

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken) {
		return this.Ok(await this.sender.Send(new GetCategoryQuery() {
			Id = id
		}, cancellationToken));
	}
}