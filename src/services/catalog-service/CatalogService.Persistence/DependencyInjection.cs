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

namespace CatalogService.Persistence;
public static class DependencyInjection {
	public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration) {
		services.AddDbContext<CategoryServiceDbContext>(options => {
			options.UseSqlServer(configuration.GetConnectionString("MsSQLConnectionString"));
		});

		services.Configure<SQLServerOptions>(configuration.GetSection(SQLServerOptions.SECTION_NAME));
		SQLServerOptions sqlOptions = services.BuildServiceProvider().GetRequiredService<IOptions<SQLServerOptions>>().Value;

		services.AddDbContext<SocialAppDbContext>(options => {
			options.UseSqlServer(configuration.GetConnectionString(MsSQLDatabaseContext.MsSQL_CONNECTION_STRING),
				 sqlServerOptions => {
					 sqlServerOptions.EnableRetryOnFailure(sqlOptions.RetryCount, TimeSpan.FromSeconds(sqlOptions.RetryDelaySeconds), null);
					 sqlServerOptions.MigrationsAssembly(typeof(SocialAppDbContext).Assembly.FullName);
					 sqlServerOptions.MigrationsHistoryTable(
						 tableName: HistoryRepository.DefaultTableName,
						 schema: SocialAppDbContext.DEFAULT_SCHEMA);
				 });

			//using var context = new SocialAppDbContext(options.Options);
			//context.Database.Migrate();
		});

		//Task.Run(async () => {
		//	await services.BuildServiceProvider().GetRequiredService<SocialAppDbContext>().Database.MigrateAsync();
		//});

		services.BuildServiceProvider().GetRequiredService<SocialAppDbContext>().Database.Migrate();

		services.AddServices()
			.AddRepositories();

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