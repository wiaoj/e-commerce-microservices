using BuildingBlocks.Application.Abstraction.Pagination;

namespace BuildingBlocks.Application.Pagination;
public record Paginate<TEntity> : IPaginate<TEntity> {
	public PaginationInfo PaginationInfo { get; set; }
	public IList<TEntity> Items { get; set; }

	public Paginate() {
		this.Items = Array.Empty<TEntity>();
	}

	public Paginate(IEnumerable<TEntity> source, Int32 index, Int32 size, Int32 from) : this() {
		if(from > index) {
			throw new ArgumentException($"indexFrom: {from} > pageIndex: {index}, must indexFrom less than or equal to pageIndex");
		}

		PaginationInfo = new PaginationInfo {
			Index = index,
			Size = size,
			From = from,
		};

		if(source is IQueryable<TEntity> queryable) {
			this.SetCount(queryable.LongCount())
				.SetItems(queryable);
		} else {
			TEntity[] enumerable = source as TEntity[] ?? source.ToArray();
			this.SetCount(enumerable.LongCount())
				.SetItems(enumerable);
		}
	}

	private Paginate<TEntity> SetCount(Int32 count) {
		PaginationInfo = new PaginationInfo() with {
			Count = count
		};

		return this;
	}

	private Paginate<TEntity> SetItems(IEnumerable<TEntity> entities) {
		this.Items = entities
			.Skip((this.PaginationInfo.Index - this.PaginationInfo.From) * this.PaginationInfo.Size)
			.Take(this.PaginationInfo.Size).ToList();
		return this;
	}
}