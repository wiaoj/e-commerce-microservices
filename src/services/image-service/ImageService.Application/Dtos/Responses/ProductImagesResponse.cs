namespace ImageService.Application.Dtos.Responses;
public sealed record ProductImagesResponse {
	public Guid ProductId { get; init; }
	public String Path { get; init; }
	public String Name { get; init; }
	public Boolean Showcase { get; init; }
}