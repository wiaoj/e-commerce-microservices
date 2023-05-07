namespace BuildingBlocks.Application.Pagination;
public sealed record PaginationRequest {
	public UInt32 Page { get; set; }
	public UInt64 Size { get; set; }

	public PaginationRequest() {
		this.Page = 1;
		this.Size = 10;
	}
};
public sealed record PaginationResponse<T>() : Paginate<T>;