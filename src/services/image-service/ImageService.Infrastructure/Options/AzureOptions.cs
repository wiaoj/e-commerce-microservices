namespace ImageService.Infrastructure.Options;
public sealed class AzureOptions : IOptions {
	public String ConnectionString { get; set; } = null!;
	public AzureSettings Options { get; set; } = null!;
}
public class AzureSettings : ISettings {
	public Boolean IsActive { get; set; } = default;
	public String ContainerName { get; set; } = null!;
}