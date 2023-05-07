namespace BuildingBlocks.Persistence.EFCore.Parameters;
public class PaginationOptions {
	public Int32 Index { get; init; } = 1;
	public Int32 Size { get; init; } = 10;
	public Int32 From { get; init; } = 0;
}