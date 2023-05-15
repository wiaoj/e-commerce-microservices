using ImageService.Application.Dtos.Requests;
using ImageService.Application.Dtos.Responses;

namespace ImageService.Application.Services;
public interface IProductImageService {
	public Task UploadImages(UploadImageRequest uploadImageRequest, CancellationToken cancellationToken);
	public Task DeleteImage(Guid productId, Guid imageId, CancellationToken cancellationToken);
	public Task DeleteImages(Guid productId, IEnumerable<Guid> imageIds, CancellationToken cancellationToken);
	public Task SetShowcase(Guid productId, Guid imageId, CancellationToken cancellationToken);
	public Task<IEnumerable<ProductImagesResponse>> GetImagesByProductId(Guid productId, CancellationToken cancellationToken);
}