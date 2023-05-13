using ImageService.Application.Storage;
using ImageService.Infrastructure.Consumers;
using ImageService.Infrastructure.Storage;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ImageService.Infrastructure;
public static class DependencyInjection {
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) {

		services.AddMassTransit(busRegistrationConfigurator => {
			busRegistrationConfigurator.SetKebabCaseEndpointNameFormatter();

			busRegistrationConfigurator.AddConsumers(Assembly.GetExecutingAssembly());
			busRegistrationConfigurator.AddSagaStateMachines(Assembly.GetExecutingAssembly());
			busRegistrationConfigurator.AddSagas(Assembly.GetExecutingAssembly());
			busRegistrationConfigurator.AddActivities(Assembly.GetExecutingAssembly());

			busRegistrationConfigurator.UsingRabbitMq((context, configurator) => {
				configurator.Host(configuration.GetConnectionString("RabbitMQ"));
				configurator.ConfigureEndpoints(context);
			});
		});

		services.AddServices();

		return services;
	}

	private static IServiceCollection AddServices(this IServiceCollection services) {
		return services.AddTransient<IStorageService, ImageStorageService>()
					   .AddStorage<LocalStorage>();
	}

	private static IServiceCollection AddStorage<StorageType>(this IServiceCollection services) where StorageType : class, IStorage {
		return services.AddScoped<IStorage, StorageType>();
	}
}