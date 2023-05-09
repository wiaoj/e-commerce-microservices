using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Persistence.EFCore.Repositories;
public abstract class EFRepository<TContext> where TContext : DbContext {
	public TContext Context { get; }

	public EFRepository(TContext context) {
		this.Context = context;
	}
}