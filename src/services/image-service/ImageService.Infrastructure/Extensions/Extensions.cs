using Microsoft.Extensions.Configuration;

namespace ImageService.Infrastructure.Extensions;
public static class Extensions {
	public static IConfigurationSection GetStorage(this IConfiguration configuration, String storageType) {
		return configuration.GetSection($"Storage:{storageType}");
	}

	//public static String WithProductMediaImagesPath(this Guid value) {
	//	return value.ToString().WithProductMediaImagesPath();
	//}

	//public static String WithProductMediaImagesPath(this String value) {
	//	return $"product/media/images/{value}";
	//}
}