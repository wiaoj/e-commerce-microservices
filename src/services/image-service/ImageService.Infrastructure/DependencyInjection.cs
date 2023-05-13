using ImageService.Application.Extensions;
using ImageService.Application.Storage;
using ImageService.Infrastructure.Extensions;
using ImageService.Infrastructure.Options;
using ImageService.Infrastructure.Storage;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace ImageService.Infrastructure;
public static class DependencyInjection {
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
															IConfiguration configuration) {

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
		services.AddStorage(configuration);

		return services;
	}

	private static IServiceCollection AddServices(this IServiceCollection services) {
		return services.AddTransient<IStorageService, ImageStorageService>();
	}

	private static IServiceCollection AddStorage(this IServiceCollection services,
											  IConfiguration configuration) {


		services.Configure<LocalOptions>(configuration.GetStorage("Local"));
		services.Configure<AwsOptions>(configuration.GetStorage("Aws"));
		services.Configure<AzureOptions>(configuration.GetStorage("Azure"));
		LocalOptions localOptions =
			services.BuildServiceProvider().GetRequiredService<IOptions<LocalOptions>>().Value;
		if(localOptions.Options.IsActive) {
			services.AddScoped<IStorage, LocalStorage>();
			services.AddScoped<ILocalStorage, LocalStorage>();
			Directory.CreateDirectory($"{localOptions.Options.Path}product/media/images/");
			return services;
		}

		AwsOptions awsOptions =
			services.BuildServiceProvider().GetRequiredService<IOptions<AwsOptions>>().Value;
		if(awsOptions.Options.IsActive) {
			return services;
		}


		AzureOptions azureOptions =
			services.BuildServiceProvider().GetRequiredService<IOptions<AzureOptions>>().Value;
		if(azureOptions.Options.IsActive) {
			services.AddScoped<IStorage, AzureStorage>();
			services.AddScoped<IAzureStorage, AzureStorage>();
			return services;
		}

		services.AddScoped<IStorage, LocalStorage>();
		services.AddScoped<ILocalStorage, LocalStorage>();
		return services;
	}
}