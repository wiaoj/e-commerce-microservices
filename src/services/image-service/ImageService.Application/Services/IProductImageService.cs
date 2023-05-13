using ImageService.Application.Dtos.Requests;

namespace ImageService.Application.Services;
public interface IProductImageService {
	public Task UploadImages(UploadImageRequest uploadImageRequest, CancellationToken cancellationToken);
	public Task DeleteImage(Guid productId, Guid imageId);
	public Task DeleteImages(Guid productId, IEnumerable<Guid> imageIds); 
	public Task SetShowcase(Guid productId, Guid imageId, CancellationToken cancellationToken);
}