using BuildingBlocks.Persistence.EFCore.Repositories;
using ImageService.Domain.Entities;
using ImageService.Persistence.Contexts;
using ImageService.Persistence.Repositories.Interfaces;

namespace ImageService.Persistence.Repositories;
public sealed class ProductWriteRepository : EFAsyncWriteRepository<ProductEntity, ImageServiceDbContext>, IProductWriteRepository {
	public ProductWriteRepository(ImageServiceDbContext context) : base(context) { }
}