using Microsoft.AspNetCore.Http;

namespace ImageService.Application.Storage;
public interface IStorage {
	public Task<List<(String fileName, String path)>> UploadAsync(String path, IFormFileCollection files);
	public Task DeleteAsync(String path, String fileName);
	public List<String> GetFiles(String path);
	public Boolean HasFile(String path, String fileName);
}