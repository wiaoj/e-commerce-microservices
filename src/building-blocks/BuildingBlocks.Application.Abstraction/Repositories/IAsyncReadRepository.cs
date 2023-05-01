using BuildingBlocks.Domain;

namespace BuildingBlocks.Application.Abstraction.Repositories;
public interface IAsyncReadRepository<TEntity> where TEntity : Entity {
	public Int64 Count { get; }
}