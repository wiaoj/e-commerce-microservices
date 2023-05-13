namespace ImageService.Infrastructure.Options;
public sealed class LocalOptions : IOptions {
	public LocalSettings Options { get; set; } = null!;

	public String GetCombinedPath(String path) {
		var isLocalSettingsPathEndsWithSlash =
			this.Options.Path.EndsWith('/')
			? this.Options.Path
			: $"{this.Options.Path}/";
		return $"{isLocalSettingsPathEndsWithSlash}{path}";
	}
}
public class LocalSettings : ISettings {
	public Boolean IsActive { get; set; } = default;
	public String Path { get; set; } = null!;
}