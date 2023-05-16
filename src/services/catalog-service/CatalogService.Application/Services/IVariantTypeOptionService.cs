using CatalogService.Application.Dtos.Requests.VariantTypeOption;

namespace CatalogService.Application.Services;
public interface IVariantTypeOptionService {
	public Task AddVariantTypeOption(
		CreateVariantTypeOptionRequest createVariantTypeOptionRequest,
		CancellationToken cancellationToken);
}