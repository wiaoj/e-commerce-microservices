using BuildingBlocks.Persistence.EFCore.Repositories;
using ImageService.Domain.Entities;
using ImageService.Persistence.Contexts;
using ImageService.Persistence.Repositories.Interfaces;

namespace ImageService.Persistence.Repositories;
public sealed class ProductImageReadRepository :
	EFAsyncReadRepository<ProductImageEntity, ImageServiceDbContext>, IProductImageReadRepository {
	public ProductImageReadRepository(ImageServiceDbContext context) : base(context) { }
}