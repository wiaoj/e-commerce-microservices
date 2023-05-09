using AutoMapper;
using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;

namespace CatalogService.Persistence.MappingProfiles;
internal class DefaultProfile : Profile {
	public DefaultProfile() {
		CreateMap(typeof(IPaginate<>), typeof(Paginate<>));
	}
}