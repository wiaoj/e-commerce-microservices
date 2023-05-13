using AutoMapper;
using CatalogService.Application.Dtos.Requests.Product;
using CatalogService.Application.Dtos.Responses.Product;
using CatalogService.Domain.Entities;
using MessagingContracts.Catalog;

namespace CatalogService.Persistence.MappingProfiles;
internal sealed class ProductProfile : Profile {
	public ProductProfile() {
		this.CreateMap<CreateProductRequest, ProductEntity>();
		this.CreateMap<UpdateProductRequest, ProductEntity>();

		this.CreateMap<ProductEntity, GetProductResponse>();

			
		this.CreateMap<ProductEntity, ProductCreated>()
			.ForMember(destination => destination.CategoryIds,
			options => options.MapFrom(productEntity => productEntity.Categories.Select(x => x.Id)));
		this.CreateMap<ProductEntity, ProductDeleted>();
	}
}