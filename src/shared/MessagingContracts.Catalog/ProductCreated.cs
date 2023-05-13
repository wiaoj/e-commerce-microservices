namespace MessagingContracts.Catalog;
public sealed record ProductCreated {
	public Guid Id { get; init; }
	public String Name { get; init;}
	public String? Description { get; init;}
	public Decimal Price { get; init;}
	public UInt16 Stock { get; init;}
	public IEnumerable<Guid>? CategoryIds { get; init;}
}