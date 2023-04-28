using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BuildingBlocks.Persistence.EFCore.Context;
public abstract class EFCoreDbContext : DbContext {
	protected EFCoreDbContext(DbContextOptions options) : base(options) { }

	public sealed override Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default) {
		UpdateEntityTimestamps();

		return base.SaveChangesAsync(cancellationToken);
	}

	private void UpdateEntityTimestamps() {
		foreach(EntityEntry<Entity> data in this.ChangeTracker.Entries<Entity>()) {
			_ = data.State switch {
				EntityState.Added => data.Entity.CreatedAt = DateTime.UtcNow,
				EntityState.Modified => data.Entity.UpdatedAt = DateTime.UtcNow,
				_ => null,
			};
		}
	}
}