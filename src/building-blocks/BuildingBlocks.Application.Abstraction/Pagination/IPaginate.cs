namespace BuildingBlocks.Application.Abstraction.Pagination;
public interface IPaginate<TEntity> {
	public PaginationInfo PaginationInfo { get; }
	public IList<TEntity> Items { get; }
}