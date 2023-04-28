using BuildingBlocks.Application.Abstraction.Repositories;

namespace BuildingBlocks.Persistence.EFCore.Repositories.Interfaces;
public interface IEFAsyncWriteRepository<in TEntity> : IAsyncWriteRepository<TEntity> where TEntity : class, new() { }