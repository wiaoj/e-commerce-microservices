using Microsoft.AspNetCore.Http;

namespace ImageService.Application.Storage;
public interface ILocalStorage : IStorage {
	public Task<Boolean> CopyFileAsync(String path, IFormFile file);
	public Task DeletePath(String path);
}