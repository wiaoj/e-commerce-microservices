using BuildingBlocks.Persistence.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Persistence.EFCore.MSSQL.Context;
public abstract class EFCoreMSSQLDbContext : EFCoreDbContext {
	public const String Name = nameof(EFCoreMSSQLDbContext);

	protected EFCoreMSSQLDbContext(DbContextOptions options) : base(options) { }
}