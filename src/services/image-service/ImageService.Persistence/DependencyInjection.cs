﻿using BuildingBlocks.Persistence.EFCore.MSSQL;
using ImageService.Application.Services;
using ImageService.Persistence.Contexts;
using ImageService.Persistence.Repositories;
using ImageService.Persistence.Repositories.Interfaces;
using ImageService.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ImageService.Persistence;
public static class DependencyInjection {
	public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration) {
		services.AddDbContext<ImageServiceDbContext>(options => {
			options.UseSqlServer(configuration.GetConnectionString("MsSQLConnectionString"));
		});

		services.Configure<SQLServerOptions>(configuration.GetSection(new SQLServerOptions().SECTION_NAME));

		SQLServerOptions sqlOptions = services.BuildServiceProvider().GetRequiredService<IOptions<SQLServerOptions>>().Value;

		services.AddDbContext<ImageServiceDbContext>(options => {
			options.UseSqlServer(configuration.GetConnectionString(ImageServiceDbContext.CONNECTION_STRING_NAME),
				 sqlServerOptions => {
					 sqlServerOptions.EnableRetryOnFailure(sqlOptions.RetryCount, TimeSpan.FromSeconds(sqlOptions.RetryDelaySeconds), null);
					 sqlServerOptions.MigrationsAssembly(typeof(ImageServiceDbContext).Assembly.FullName);
					 sqlServerOptions.MigrationsHistoryTable(
						 tableName: HistoryRepository.DefaultTableName,
						 schema: "test");
				 });
		});

		services.BuildServiceProvider().GetRequiredService<ImageServiceDbContext>().Database.Migrate();

		services.AddServices()
				.AddRepositories();

		return services;
	}

	private static IServiceCollection AddServices(this IServiceCollection services) {
		return services.AddScoped<IProductService, ProductService>()
					   .AddScoped<IProductImageService, ProductImageService>();
	}

	private static IServiceCollection AddRepositories(this IServiceCollection services) {
		return services.AddScoped<IProductWriteRepository, ProductWriteRepository>()
					   .AddScoped<IProductReadRepository, ProductReadRepository>()
					   .AddScoped<IProductImageWriteRepository, ProductImageWriteRepository>()
					   .AddScoped<IProductImageReadRepository, ProductImageReadRepository>();
	}
}