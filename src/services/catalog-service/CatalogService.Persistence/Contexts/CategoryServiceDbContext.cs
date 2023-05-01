using BuildingBlocks.Persistence.EFCore.MSSQL.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Persistence.Contexts;
public sealed class CategoryServiceDbContext : EFCoreMSSQLDbContext {
	public CategoryServiceDbContext(DbContextOptions options) : base(options) {	}
}