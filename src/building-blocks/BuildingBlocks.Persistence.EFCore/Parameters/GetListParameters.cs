using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BuildingBlocks.Persistence.EFCore.Parameters;
public sealed class GetListParameters<TEntity> : ReadParameters {
	public Expression<Func<TEntity, Boolean>>? Predicate { get; init; }
	public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy { get; init; }
	public Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, Object>>? Include { get; init; }
}