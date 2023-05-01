using BuildingBlocks.Application.Abstraction.Repositories;
using BuildingBlocks.Domain;

namespace BuildingBlocks.Persistence.EFCore.Repositories.Interfaces;
public interface IEFAsyncWriteRepository<in TEntity> : IAsyncWriteRepository<TEntity> where TEntity : Entity { }