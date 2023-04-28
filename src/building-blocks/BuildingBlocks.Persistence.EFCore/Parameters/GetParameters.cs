using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BuildingBlocks.Persistence.EFCore.Parameters;
public sealed class GetParameters<TEntity> : ReadParameters {
	public required Expression<Func<TEntity, Boolean>> Predicate { get; set; }
	public Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, Object>>? Include { get; set; }
}