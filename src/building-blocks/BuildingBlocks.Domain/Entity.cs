namespace BuildingBlocks.Domain;
public abstract class Entity {
	public Guid Id { get; private set; }
	public DateTime CreatedAt { get; private set; }

	public Entity() {
		this.Id = Guid.NewGuid();
		this.CreatedAt = DateTime.UtcNow;
	}
}