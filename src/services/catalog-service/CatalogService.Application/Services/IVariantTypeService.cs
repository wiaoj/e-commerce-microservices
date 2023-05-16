using CatalogService.Application.Dtos.Requests.VariantType;

namespace CatalogService.Application.Services;
public interface IVariantTypeService {
	public Task AddVariantAsync(
		CreateVariantTypeRequest createVariantTypeRequest,
		CancellationToken cancellationToken);
}