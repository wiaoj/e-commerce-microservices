namespace BuildingBlocks.Application.Abstraction.Repositories;
public interface IAsyncWriteRepository<in TEntity> where TEntity : class, new() {
	public ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken);
	public ValueTask AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
	public ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken);
	public ValueTask UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
	public ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken);
	public ValueTask DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
}