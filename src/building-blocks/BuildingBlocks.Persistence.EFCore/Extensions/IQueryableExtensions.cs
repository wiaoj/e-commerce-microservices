using BuildingBlocks.Application.Abstraction.Pagination;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain;
using BuildingBlocks.Persistence.EFCore.Parameters;
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
														   PaginationOptions? paginationOptions,
														   CancellationToken cancellationToken) {
		paginationOptions ??= new();
		if(paginationOptions.From > paginationOptions.Index) {
			throw new ArgumentException($"From: {paginationOptions.From} > Index: {paginationOptions.Index}, must from <= Index");
		}

		Int64 count = await source.LongCountAsync(cancellationToken).ConfigureAwait(false);

		List<TEntity> items = await source
			.Skip((paginationOptions.Index - 1 - paginationOptions.From) * paginationOptions.Size)
			.Take(paginationOptions.Size)
			.ToListAsync(cancellationToken)
			.ConfigureAwait(false);

		return new Paginate<TEntity>() {
			PaginationInfo = new() {
				Index = paginationOptions.Index,
				Size = paginationOptions.Size,
				From = paginationOptions.From,
				Count = count,
			},
			Items = items,
		};
	}
}