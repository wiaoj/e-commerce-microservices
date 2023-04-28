namespace BuildingBlocks.Application.Abstraction.Repositories;
public interface IAsyncReadRepository<TEntity> where TEntity : class, new() {
	public Int64 Count { get; }
}