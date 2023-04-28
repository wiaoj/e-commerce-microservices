using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Domain;
using BuildingBlocks.Persistence.EFCore.Parameters;
using BuildingBlocks.Persistence.EFCore.Extensions;
using BuildingBlocks.Persistence.EFCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Persistence.EFCore.Repositories;
public abstract class EFAsyncReadRepository<TEntity, TContext> : EFRepository<TContext>, IEFAsyncReadRepository<TEntity>
	where TEntity : Entity, new()
	where TContext : DbContext, new() {

	protected EFAsyncReadRepository(TContext context) : base(context) { }

	public IQueryable<TEntity> EntityTable => this.Context.Set<TEntity>();

	public Int64 Count => this.EntityTable.LongCount();

	public async Task<TEntity?> GetAsync(GetParameters<TEntity> parameters) {
		return await this.EntityTable.ApplyTracking(parameters.EnableTracking)
							   .ApplyInclude(parameters.Include)
							   .FirstOrDefaultAsync(parameters.Predicate, parameters.CancellationToken);
	}

	public async Task<IPaginate<TEntity>> GetListAsync(GetListParameters<TEntity> parameters) {
		return await this.EntityTable.ApplyTracking(parameters.EnableTracking)
						  .ApplyInclude(parameters.Include)
						  .ApplyPredicate(parameters.Predicate)
						  .ApplyOrderBy(parameters.OrderBy)
						  .ToPaginateAsync(parameters.Index, parameters.Size, default, parameters.CancellationToken);
	}
}