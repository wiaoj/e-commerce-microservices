using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Abstraction.Repositories;
using BuildingBlocks.Domain;
using BuildingBlocks.Persistence.EFCore.Parameters;

namespace BuildingBlocks.Persistence.EFCore.Repositories.Interfaces;
public interface IEFAsyncReadRepository<TEntity> : IAsyncReadRepository<TEntity> where TEntity : Entity {
	public Task<TEntity?> GetAsync(GetParameters<TEntity> parameters);
	public Task<IPaginate<TEntity>> GetPaginatedListAsync(GetPaginatedListParameters<TEntity> parameters); 
	public Task<IQueryable<TEntity>> GetListAsync(GetListParameters<TEntity> parameters)
}