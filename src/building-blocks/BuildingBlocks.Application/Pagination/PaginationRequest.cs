namespace BuildingBlocks.Application.Pagination;
public sealed record PaginationRequest(Int32 Page = 1, Int32 Size = 10);
public sealed record PaginationResponse<T>() : Paginate<T>;