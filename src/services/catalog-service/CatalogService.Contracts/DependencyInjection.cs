using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CatalogService.Contracts;
public static class DependencyInjection {
	public static IServiceCollection AddContracts(this IServiceCollection services) {
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		return services;
	}
}