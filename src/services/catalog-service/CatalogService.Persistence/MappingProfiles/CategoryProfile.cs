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

		CreateMap<CategoryEntity, CategoryDto>()
			.ForMember(destination => destination.Id,
			  options => options.MapFrom(source => source.Id))
			.ForMember(destination => destination.Name,
			  options => options.MapFrom(source => source.Name))
			/*
			.ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryId))
			.ForMember(dest => dest.ChildCategories, opt => opt.MapFrom(src => src.ChildCategories))
			*/;

		CreateMap<CategoryEntity, GetCategoryDto>()
			.ForMember(destination => destination.ChildCategories,
			  options => options.MapFrom(source => source.ChildCategories));

		CreateMap<IPaginate<CategoryEntity>, Paginate<GetCategoriesDto>>()
			.ForMember(destination => destination.Items,
			  options => options.MapFrom(source => source.Items));

		CreateMap<CategoryEntity, GetCategoriesDto>();
	}
}