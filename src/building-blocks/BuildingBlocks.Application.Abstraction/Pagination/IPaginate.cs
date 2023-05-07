namespace BuildingBlocks.Application.Abstraction.Pagination;
public interface IPaginate<TEntity> {
	public Int32 From { get; }
	public Int32 Index { get; }
	public Int32 Size { get; }
	public Int64 Count { get; }
	public Int32 Pages { get; }
	public IList<TEntity> Items { get; }
	public Boolean HasPrevious => this.Index - this.From > default(Int32);
	public Boolean HasNext => this.Index - this.From + 1 < this.Pages;
}