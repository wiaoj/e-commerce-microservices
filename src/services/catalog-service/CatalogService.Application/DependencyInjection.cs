using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CatalogService.Application;
public static class DependencyInjection {
	public static IServiceCollection AddApplicationServices(this IServiceCollection services) {
		services.AddMediatR(configuration => {
			configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
		});
		return services;
	}
}