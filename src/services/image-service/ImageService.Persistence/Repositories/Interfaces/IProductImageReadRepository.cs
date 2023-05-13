using BuildingBlocks.Application.Abstraction.Repositories;
using BuildingBlocks.Persistence.EFCore.Repositories.Interfaces;
using ImageService.Domain.Entities;

namespace ImageService.Persistence.Repositories.Interfaces;
public interface IProductImageReadRepository : IEFAsyncReadRepository<ProductImageEntity> { }