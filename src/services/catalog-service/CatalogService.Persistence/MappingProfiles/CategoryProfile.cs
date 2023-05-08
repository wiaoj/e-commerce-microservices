using AutoMapper;
using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Domain.Entities;

namespace CatalogService.Persistence.MappingProfiles;
internal sealed class CategoryProfile : Profile {
	public CategoryProfile() {
		this.CreateMap<CreateCategoryDto, CategoryEntity>();
		this.CreateMap<UpdateCategoryDto, CategoryEntity>();

		this.CreateMap<CategoryEntity, GetCategoryDto>();

		this.CreateMap<CategoryEntity, GetCategoriesDto>();

		this.CreateMap<CategoryEntity, GetCategoryWithProductsDto>();
	}
}