using Microsoft.Extensions.Configuration;

namespace ImageService.Infrastructure.Extensions;
public static class Extensions {
	public static String? GetStorageConnectionString(this IConfiguration configuration, String storageType) {
		return configuration[$"Storage:{storageType}"];
	}
}