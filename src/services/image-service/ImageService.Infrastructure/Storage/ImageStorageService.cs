using ImageService.Application.Storage;
using Microsoft.AspNetCore.Http;

namespace ImageService.Infrastructure.Storage;
public sealed class ImageStorageService : IStorageService {
	private readonly IStorage storage;

	public ImageStorageService(IStorage storage) {
		this.storage = storage;
	}

	public String StorageName => this.storage.GetType().Name;

	public Task DeleteAsync(String path, String fileName) {
		return this.storage.DeleteAsync(path, fileName);
	}

	public List<String> GetFiles(String path) {
		return this.storage.GetFiles(path);
	}

	public Boolean HasFile(String path, String fileName) {
		return this.storage.HasFile(path, fileName);
	}

	public Task<List<(String fileName, String path)>> UploadAsync(String path, IFormFileCollection files) {
		if(files.All(file => file.ContentType.Contains("image")) is false)
			throw new Exception("Sadece resim türleri kabul edilmektedir");
		
		return this.storage.UploadAsync(path, files);
	}
}