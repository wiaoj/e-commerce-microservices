namespace ImageService.Application.Storage;
public interface IStorageService : IStorage {
	public String StorageName { get; }
}