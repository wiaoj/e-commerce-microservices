using BuildingBlocks.Persistence.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Persistence.EFCore.MSSQL;
public abstract class MSSQLDbContext : EFCoreDbContext {
	public const String CONNECTION_STRING_NAME = "MsSQL";

	protected MSSQLDbContext(DbContextOptions options) : base(options) { }
}