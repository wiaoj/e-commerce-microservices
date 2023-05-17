using AutoMapper;
using CatalogService.Application.Dtos.Requests.Category;
using CatalogService.Application.Dtos.Responses.Category;
using CatalogService.Application.Dtos.Responses.Product;
using CatalogService.Domain.Entities;

namespace CatalogService.Persistence.MappingProfiles;
internal sealed class CategoryProfile : Profile {
	public CategoryProfile() {
		this.CreateMap<CreateCategoryRequest, CategoryEntity>();
		this.CreateMap<UpdateCategoryRequest, CategoryEntity>();

		this.CreateMap<CategoryEntity, GetCategoryResponse>();

		this.CreateMap<CategoryEntity, GetCategoriesResponse>();

		this.CreateMap<CategoryEntity, GetCategoryWithProductsResponse>()
			.ForMember(dest => dest.Products, opt => opt.Ignore());

		this.CreateMap<CategoryEntity, GetRootCategoriesResponse>();
	}
}