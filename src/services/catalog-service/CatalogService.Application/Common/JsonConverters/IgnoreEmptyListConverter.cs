using System.Text.Json;
using System.Text.Json.Serialization;

namespace CatalogService.Application.Common.JsonConverters;
public class IgnoreEmptyListConverter<T> : JsonConverter<List<T>> where T : class {
	public override List<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
		// List read operations are handled by the JsonSerializer
		return JsonSerializer.Deserialize<List<T>>(ref reader, options) ?? new List<T>();
	}

	public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options) {
		// Ignore the list if it is empty
		if(value == null || value.Count == 0)
			return;

		// Serialize the list as usual
		JsonSerializer.Serialize(writer, value, options);
	}
}