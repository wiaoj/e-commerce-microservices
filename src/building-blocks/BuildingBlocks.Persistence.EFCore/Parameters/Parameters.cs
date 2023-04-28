namespace BuildingBlocks.Persistence.EFCore.Parameters;
public abstract class Parameters {
	public required CancellationToken CancellationToken { get; set; }
}