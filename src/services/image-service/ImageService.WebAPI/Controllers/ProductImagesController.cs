using ImageService.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageService.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductImagesController : ControllerBase {
	private readonly IProductImageService productImageService;

	public ProductImagesController(IProductImageService productImageService) {
		this.productImageService = productImageService;
	}

	[HttpPost]
	[Route("{productId}")]
	public async Task<IActionResult> Upload([FromRoute] Guid productId, IFormFileCollection images, CancellationToken cancellationToken) {
		await this.productImageService.UploadImages(new(productId, images), cancellationToken);
		return this.Ok();
	}

	[HttpDelete]
	[Route("{productId}/{imageId}")]
	public async Task<IActionResult> Delete([FromRoute] Guid productId, [FromRoute] Guid imageId, CancellationToken cancellationToken) {
		await this.productImageService.DeleteImage(productId, imageId, cancellationToken);
		return this.Ok();
	}

	[HttpDelete]
	[Route("{productId}")]
	public async Task<IActionResult> Delete([FromRoute] Guid productId,
										 [FromBody] IEnumerable<Guid> imageIds,
										 CancellationToken cancellationToken) {
		await this.productImageService.DeleteImages(productId, imageIds, cancellationToken);
		return this.Ok();
	}

	[HttpGet]
	[Route("{imageId}")]
	public async Task<IActionResult> GetImage([FromRoute] Guid imageId, CancellationToken cancellationToken) {
		await Task.CompletedTask;
		return this.Ok();
	}

	[HttpPost]
	[Route("{productId}/{imageId}")]
	public async Task<IActionResult> SetShowcase(
		[FromRoute] Guid productId,
		[FromRoute] Guid imageId,
		CancellationToken cancellationToken) {
		await this.productImageService.SetShowcase(productId, imageId, cancellationToken);
		return this.Ok();
	}

	[HttpGet]
	[Route("[action]/{productId}")]
	public async Task<IActionResult> GetImagesByProductId([FromRoute] Guid productId, CancellationToken cancellationToken) {
		return this.Ok(await this.productImageService.GetImagesByProductId(productId, cancellationToken));
	}
}