namespace CatalogService.Domain.Events;
public sealed record ProductCreatedEvent {
	public Guid Id { get; set; }
	public String Name { get; set; }
	public Decimal Price { get; set; }
	public String Description { get; set; }
	public List<String> ImagesBase64 { get; set; }
}