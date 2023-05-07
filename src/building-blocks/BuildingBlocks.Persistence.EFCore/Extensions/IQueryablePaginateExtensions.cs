using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BuildingBlocks.Persistence.EFCore.Extensions;
public static class IQueryableExtensions {
	public static IQueryable<TEntity> ApplyTracking<TEntity>(this IQueryable<TEntity> queryable, Boolean tracking) where TEntity : Entity {
		return tracking ? queryable : queryable.AsNoTracking();
	}
	public static IQueryable<TEntity> ApplyInclude<TEntity>(this IQueryable<TEntity> queryable,
														 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, Object>>? include) {
		return include is not null ? include(queryable) : queryable;
	}
	public static IQueryable<TEntity> ApplyPredicate<TEntity>(this IQueryable<TEntity> queryable,
														   Expression<Func<TEntity, Boolean>>? predicate) {
		return predicate is not null ? queryable.Where(predicate) : queryable;
	}

	public static IQueryable<TEntity> ApplyOrderBy<TEntity>(this IQueryable<TEntity> queryable,
														 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy) {
		return orderBy is not null ? orderBy(queryable) : queryable;
	}

	public static async Task<IPaginate<TEntity>> ToPaginateAsync<TEntity>(this IQueryable<TEntity> source,
														   Int32 index,
														   Int32 size,
														   Int32 from,
														   CancellationToken cancellationToken) {
		if(from > index) {
			throw new ArgumentException($"From: {from} > Index: {index}, must from <= Index");
		}

		Int32 count = await source.CountAsync(cancellationToken).ConfigureAwait(false);

		List<TEntity> items = await source
			.Skip((index - 1 - from) * size)
			.Take(size)
			.ToListAsync(cancellationToken)
			.ConfigureAwait(false);

		return new Paginate<TEntity>() {
			Index = index,
			Size = size,
			From = from,
			Count = count,
			Items = items,
			Pages = (Int32)Math.Ceiling(count / (Double)size)
		};
	}
}