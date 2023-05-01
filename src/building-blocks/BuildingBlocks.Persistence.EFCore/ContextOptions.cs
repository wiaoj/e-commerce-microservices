namespace BuildingBlocks.Persistence.EFCore;
public abstract class DatabaseOptions<TContext> {
	public String SECTION_NAME = typeof(TContext).Name;
	public Int32 RetryCount { get; init; } = 3;
	public Int32 RetryDelaySeconds { get; init; } = 30;
}