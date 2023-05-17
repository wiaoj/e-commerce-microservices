using BuildingBlocks.Application.Pagination;
using CatalogService.Application.Dtos.Requests.Category;
using CatalogService.Application.Features.Categories.Commands.CreateCategory;
using CatalogService.Application.Features.Categories.Commands.DeleteCategory;
using CatalogService.Application.Features.Categories.Commands.UpdateCategory;
using CatalogService.Application.Features.Categories.Queries.GetCategories;
using CatalogService.Application.Features.Categories.Queries.GetCategory;
using CatalogService.Application.Features.Categories.Queries.GetCategoryWithProducts;
using CatalogService.Application.Features.Categories.Queries.GetRootCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase {
	private readonly ISender sender;

	public CategoriesController(ISender sender) {
		this.sender = sender;
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateCategoryRequest request, CancellationToken cancellationToken) {
		await this.sender.Send(new CreateCategoryCommand() {
			CreateCategoryRequest = request
		}, cancellationToken);
		return this.Ok();
	}

	[HttpPut("{id.Value}")]
	public async Task<IActionResult> Update(
		[FromRoute] CategoryIdRequest id,
		[FromBody] UpdateCategoryRequest request,
		CancellationToken cancellationToken) {
		request.Id = id.Value;
		await this.sender.Send(new UpdateCategoryCommand() {
			UpdateCategoryRequest = request
		}, cancellationToken);
		return this.Ok();
	}

	[HttpDelete("{request.id}")]
	public async Task<IActionResult> Delete(DeleteCategoryRequest request, CancellationToken cancellationToken) {
		await this.sender.Send(new DeleteCategoryCommand() {
			DeleteCategoryRequest = request
		}, cancellationToken);
		return this.Ok();
	}

	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request, CancellationToken cancellationToken) {
		return this.Ok(await this.sender.Send(new GetCategoriesQuery() {
			PaginationRequest = request
		}, cancellationToken));
	}
	
	[HttpGet]
	[Route("[action]")]
	public async Task<IActionResult> GetRootCategories(CancellationToken cancellationToken) {
		return this.Ok(await this.sender.Send(new GetRootCategoriesQuery(), cancellationToken));
	}

	[HttpGet("{id.Value}")]
	public async Task<IActionResult> GetById([FromRoute] CategoryIdRequest id, CancellationToken cancellationToken) {
		return this.Ok(await this.sender.Send(new GetCategoryQuery() {
			CategoryIdRequest = id
		}, cancellationToken));
	}

	[HttpGet("[action]/{id.Value}")]
	public async Task<IActionResult> GetByIdWithProducts(
		[FromRoute] CategoryIdRequest id,
		[FromQuery] PaginationRequest paginationRequest,
		CancellationToken cancellationToken) {
		return this.Ok(await this.sender.Send(new GetCategoryWithProductsQuery() {
			Id = id,
			PaginationRequest = paginationRequest
		}, cancellationToken));
	}
}