namespace ImageService.Application.Extensions;
public static class Extensions {
	public static String WithProductMediaImagesPath(this Guid value) {
		return value.ToString().WithProductMediaImagesPath();
	}

	public static String WithProductMediaImagesPath(this String value) {
		return $"product/media/images/{value}";
	}
}