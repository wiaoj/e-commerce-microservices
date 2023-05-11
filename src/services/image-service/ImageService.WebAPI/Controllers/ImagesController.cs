using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageService.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase {

	[HttpPost]
	[Route("{productId}")]
	public async Task<IActionResult> Upload([FromRoute] Guid productId, IFormFile image, CancellationToken cancellationToken) {
		await Task.CompletedTask;
		return this.Ok();
	}

	[HttpDelete]
	[Route("{productId}/{imageId}")]
	public async Task<IActionResult> Delete([FromRoute] Guid productId, [FromRoute] Guid imageId, CancellationToken cancellationToken) {
		await Task.CompletedTask;
		return this.Ok();
	}

	[HttpDelete]
	[Route("{productId}")]
	public async Task<IActionResult> Delete([FromRoute] Guid productId,
										 [FromBody] IEnumerable<Guid> imageIds,
										 CancellationToken cancellationToken) {
		await Task.CompletedTask;
		return this.Ok();
	}

	[HttpGet]
	[Route("{imageId}")]
	public async Task<IActionResult> GetImage([FromRoute] Guid imageId, CancellationToken cancellationToken) {
		await Task.CompletedTask;
		return this.Ok();
	}

	[HttpGet]
	[Route("[action]/{productId}")]
	public async Task<IActionResult> GetImagesByProductId([FromRoute] Guid productId, CancellationToken cancellationToken) {
		await Task.CompletedTask;
		return this.Ok();
	}
}