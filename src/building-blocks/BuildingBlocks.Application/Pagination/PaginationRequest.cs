namespace BuildingBlocks.Application.Pagination;
public sealed record PaginationRequest(Int32 Page, Int32 Size);
public sealed record PaginationResponse<T>() : Paginate<T>;