namespace CatalogService.Application.Dtos.Requests.VariantTypeOption;
public sealed record CreateVariantTypeOptionRequest(String Value,  Guid VariantTypeId) : IRequestModel;