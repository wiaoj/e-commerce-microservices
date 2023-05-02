using AutoMapper;
using CatalogService.Application.Features.Categories.Dtos;

namespace CatalogService.Contracts;
public class Mapping {
	private readonly IMapper mapper;

	public Mapping(IMapper mapper) {
		this.mapper = mapper;
	}

	public UpdateCategoryDto UpdateCategoryDto(CategoryId id, UpdateCategory category) {
		UpdateCategoryDto mappedCategoryDto = this.mapper.Map<UpdateCategoryDto>(id);
		return this.mapper.Map(category, mappedCategoryDto);
	}
}