namespace BuildingBlocks.Application.Abstraction.Pagination;

public sealed record PaginationInfo {
	public Int32 Index { get; init; }
	public Int32 Size { get; init; }
	public Int32 From { get; init; }
	public Int64 Count { get; init; }
	public Int32 Pages => (Int32)Math.Ceiling(this.Count / (Double)this.Size);
	public Boolean HasPrevious => this.Index - this.From > default(Int32);
	public Boolean HasNext => this.Index - this.From + 1 < this.Pages;
}