namespace BuildingBlocks.Domain;
public abstract class Entity {
	public Guid Id { get; private set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }

	protected Entity() {
		this.Id = Guid.NewGuid();
	}
}