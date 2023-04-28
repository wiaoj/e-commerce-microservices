using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Abstraction.Repositories;
using BuildingBlocks.Persistence.EFCore.Parameters;

namespace BuildingBlocks.Persistence.EFCore.Repositories.Interfaces;
public interface IEFAsyncReadRepository<TEntity> : IAsyncReadRepository<TEntity> where TEntity : class, new() {
	public Task<TEntity?> GetAsync(GetParameters<TEntity> parameters);
	public Task<IPaginate<TEntity>> GetListAsync(GetListParameters<TEntity> parameters);
}