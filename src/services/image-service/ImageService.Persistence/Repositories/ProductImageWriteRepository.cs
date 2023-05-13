using BuildingBlocks.Persistence.EFCore.Repositories;
using ImageService.Domain.Entities;
using ImageService.Persistence.Contexts;
using ImageService.Persistence.Repositories.Interfaces;

namespace ImageService.Persistence.Repositories;
public sealed class ProductImageWriteRepository : 
	EFAsyncWriteRepository<ProductImageEntity, ImageServiceDbContext>, IProductImageWriteRepository {
	public ProductImageWriteRepository(ImageServiceDbContext context) : base(context) { }
}