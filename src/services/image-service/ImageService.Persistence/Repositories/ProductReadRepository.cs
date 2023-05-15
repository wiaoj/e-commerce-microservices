using BuildingBlocks.Persistence.EFCore.Repositories;
using ImageService.Domain.Entities;
using ImageService.Persistence.Contexts;
using ImageService.Persistence.Repositories.Interfaces;

namespace ImageService.Persistence.Repositories;
public sealed class ProductReadRepository : EFAsyncReadRepository<ProductEntity, ImageServiceDbContext>, IProductReadRepository {
	public ProductReadRepository(ImageServiceDbContext context) : base(context) { }
}