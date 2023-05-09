using AutoMapper;
using CatalogService.Application.Dtos.Requests.Product;
using CatalogService.Application.Dtos.Responses.Product;
using CatalogService.Domain.Entities;

namespace CatalogService.Persistence.MappingProfiles;
internal sealed class ProductProfile : Profile {
	public ProductProfile() {
		this.CreateMap<CreateProductRequest, ProductEntity>();
		this.CreateMap<UpdateProductRequest, ProductEntity>();

		this.CreateMap<ProductEntity, GetProductResponse>();
	}
}