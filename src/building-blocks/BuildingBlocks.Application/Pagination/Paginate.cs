using BuildingBlocks.Application.Abstraction.Pagination;

namespace BuildingBlocks.Application.Pagination;
public record Paginate<TEntity> : IPaginate<TEntity> {
	public Int32 Index { get; set; }
	public Int32 Size { get; set; }
	public Int32 From { get; set; }
	public Int32 Pages { get; set;}
	public Int64 Count { get; set; }
	public IList<TEntity> Items { get; set; }
	public Boolean HasPrevious => this.Index - this.From > default(Int32);
	public Boolean HasNext => this.Index - this.From + 1 < this.Pages;

	public Paginate() {
		this.Items = Array.Empty<TEntity>();
	}

	public Paginate(IEnumerable<TEntity> source, Int32 index, Int32 size, Int32 from) : this() {
		if(from > index) {
			throw new ArgumentException($"indexFrom: {from} > pageIndex: {index}, must indexFrom less than or equal to pageIndex");
		}

		this.Index = index;
		this.Size = size;
		this.From = from;
		this.Pages = (Int32)Math.Ceiling(this.Count / (Double)this.Size);

		if(source is IQueryable<TEntity> queryable) {
			this.SetCount(queryable.LongCount())
				.SetItems(queryable);
		} else {
			TEntity[] enumerable = source as TEntity[] ?? source.ToArray();
			this.SetCount(enumerable.LongCount())
				.SetItems(enumerable);
		}
	}

	private Paginate<TEntity> SetCount(Int64 count) {
		this.Count = count;
		return this;
	}

	private Paginate<TEntity> SetItems(IEnumerable<TEntity> entities) {
		this.Items = entities.Skip((this.Index - this.From) * this.Size).Take(this.Size).ToList();
		return this;
	}
}