using CatalogService.Application.Common.JsonConverters;
using CatalogService.Application.Features.Categories.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json.Serialization;

namespace CatalogService.Application;
public static class DependencyInjection {
	public static IServiceCollection AddApplicationServices(this IServiceCollection services) {
		services.AddMediatR(configuration => {
			configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
		});
		return services;
	}

	public static IList<JsonConverter> AddApplicationJsonConverters(this IList<JsonConverter> converters) {
		converters.Add(new IgnoreEmptyListConverter<GetCategoryDto>());
		return converters;
	}
}