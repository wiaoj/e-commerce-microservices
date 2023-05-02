using AutoMapper;
using CatalogService.Application.Features.Categories.Dtos;

namespace CatalogService.Contracts;
internal class MappingProfile : Profile {
	public MappingProfile() {
		CreateMap<CategoryId, Guid>()
			.ForMember(destionation => destionation,
			  options => options.MapFrom(x => x.Value));

		CreateMap<UpdateCategory, UpdateCategoryDto>();
	}
}