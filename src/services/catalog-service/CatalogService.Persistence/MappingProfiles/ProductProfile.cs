using AutoMapper;
using CatalogService.Application.Features.Products.Dtos;
using CatalogService.Domain.Entities;

namespace CatalogService.Persistence.MappingProfiles;
internal sealed class ProductProfile : Profile {
	public ProductProfile() {
		CreateMap<CreateProductDto, ProductEntity>();

		CreateMap<ProductEntity, GetProductDto>();
	}
}