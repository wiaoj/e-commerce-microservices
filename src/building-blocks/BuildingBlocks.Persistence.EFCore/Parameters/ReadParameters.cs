namespace BuildingBlocks.Persistence.EFCore.Parameters;
public abstract class ReadParameters : Parameters {
	public Boolean EnableTracking { get; set; } = true;
}