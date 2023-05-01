using CatalogService.Application.Features.Categories.Commands.CreateCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
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
	public IActionResult Create(CreateCategoryCommand request) {
		this.sender.Send(request);
		return Ok();
	}
}