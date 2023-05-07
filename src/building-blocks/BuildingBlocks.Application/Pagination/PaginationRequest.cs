namespace BuildingBlocks.Application.Pagination;
public sealed record PaginationRequest(UInt32 Page = 1, UInt64 Size = 10);
public sealed record PaginationResponse<T>() : Paginate<T>;