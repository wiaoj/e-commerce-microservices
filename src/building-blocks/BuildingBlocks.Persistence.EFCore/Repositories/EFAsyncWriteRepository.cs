using BuildingBlocks.Domain;
using BuildingBlocks.Persistence.EFCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Persistence.EFCore.Repositories;
public abstract class EFAsyncWriteRepository<TEntity, TContext> : EFRepository<TContext>, IEFAsyncWriteRepository<TEntity>
	where TEntity : Entity
	where TContext : DbContext {
	protected EFAsyncWriteRepository(TContext context) : base(context) { }

	public async ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken) {
		await this.Context.AddAsync(entity, cancellationToken);
	}

	public async ValueTask AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) {
		await this.Context.AddRangeAsync(entities, cancellationToken);
	}

	public async ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken) {
		cancellationToken.ThrowIfCancellationRequested();
		this.Context.Remove(entity);
		await Task.CompletedTask;
	}

	public async ValueTask DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) {
		cancellationToken.ThrowIfCancellationRequested();
		this.Context.RemoveRange(entities);
		await Task.CompletedTask;
	}

	public async ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken) {
		cancellationToken.ThrowIfCancellationRequested();
		this.Context.Update(entity);
		await Task.CompletedTask;
	}

	public async ValueTask UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) {
		cancellationToken.ThrowIfCancellationRequested();
		this.Context.UpdateRange(entities);
		await Task.CompletedTask;
	}

	public Task<Int32> SaveChangesAsync(CancellationToken cancellationToken) {
		return this.Context.SaveChangesAsync(cancellationToken);
	}
}