using BuildingBlocks.Persistence.EFCore.MSSQL;
using CatalogService.Application.Services;
using CatalogService.Persistence.Contexts;
using CatalogService.Persistence.Repositories;
using CatalogService.Persistence.Repositories.Interfaces;
using CatalogService.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace CatalogService.Persistence;
public static class DependencyInjection {
	public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration) {
		services.AddDbContext<CategoryServiceDbContext>(options => {
			options.UseSqlServer(configuration.GetConnectionString("MsSQLConnectionString"));
		});

		services.Configure<SQLServerOptions>(configuration.GetSection(new SQLServerOptions().SECTION_NAME));

		SQLServerOptions sqlOptions = services.BuildServiceProvider().GetRequiredService<IOptions<SQLServerOptions>>().Value;

		services.AddDbContext<CategoryServiceDbContext>(options => {
			options.UseSqlServer(configuration.GetConnectionString(CategoryServiceDbContext.CONNECTION_STRING_NAME),
				 sqlServerOptions => {
					 sqlServerOptions.EnableRetryOnFailure(sqlOptions.RetryCount, TimeSpan.FromSeconds(sqlOptions.RetryDelaySeconds), null);
					 sqlServerOptions.MigrationsAssembly(typeof(CategoryServiceDbContext).Assembly.FullName);
					 sqlServerOptions.MigrationsHistoryTable(
						 tableName: HistoryRepository.DefaultTableName,
						 schema: "test");
				 });
		});

		services.BuildServiceProvider().GetRequiredService<CategoryServiceDbContext>().Database.Migrate();

		services.AddServices()
			.AddRepositories();

		services.AddAutoMapper(Assembly.GetExecutingAssembly());

		return services;
	}

	private static IServiceCollection AddServices(this IServiceCollection services) {
		services.AddScoped<ICategoryService, CategoryService>();
		return services;
	}

	private static IServiceCollection AddRepositories(this IServiceCollection services) {
		services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
		services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
		return services;
	}
}