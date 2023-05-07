using AutoMapper;
using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Domain.Entities;

namespace CatalogService.Persistence.MappingProfiles;
internal sealed class CategoryProfile : Profile {
	public CategoryProfile() {
		CreateMap<CreateCategoryDto, CategoryEntity>();
		CreateMap<UpdateCategoryDto, CategoryEntity>();

		CreateMap<CategoryEntity, CategoryDto>();

		CreateMap<CategoryEntity, GetCategoryDto>();

		CreateMap<CategoryEntity, GetCategoriesDto>();
	}
}