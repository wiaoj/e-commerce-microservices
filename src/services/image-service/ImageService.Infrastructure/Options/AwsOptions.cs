namespace ImageService.Infrastructure.Options;
public sealed class AwsOptions : IOptions {
	public String ConnectionString { get; set; } = null!;
	public AwsSettings Options { get; set; } = null!;
}
public class AwsSettings : ISettings {
	public Boolean IsActive { get; set; } = default;
	public String BucketName { get; set; } = null!;
}