using BuildingBlocks.Application.Abstraction.Pagination;

namespace BuildingBlocks.Application.Pagination;
public record Paginate<TEntity> : IPaginate<TEntity> {
	public PaginationInfo PaginationInfo { get; init; } = new();
	public IList<TEntity> Items { get; set; } = Array.Empty<TEntity>();
}