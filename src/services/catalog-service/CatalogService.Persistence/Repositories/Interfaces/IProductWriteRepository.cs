using BuildingBlocks.Persistence.EFCore.Repositories.Interfaces;
using CatalogService.Domain.Entities;

namespace CatalogService.Persistence.Repositories.Interfaces;
public interface IProductWriteRepository : IEFAsyncWriteRepository<ProductEntity> { }