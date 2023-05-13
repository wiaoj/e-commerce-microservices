using ImageService.Application.Storage;
using ImageService.Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ImageService.Infrastructure.Storage;
public sealed class LocalStorage : ILocalStorage {
	private readonly LocalOptions localSettings;

	public LocalStorage(IOptions<LocalOptions> localSettings) {
		this.localSettings = localSettings.Value;
	}

	public async Task<Boolean> CopyFileAsync(String path, IFormFile file) {
		try {
			// This path already changed
			await using FileStream fileStream = new(path: path,
										   mode: FileMode.Create,
										   access: FileAccess.Write,
										   share: FileShare.None,
										   bufferSize: 1024 * 1024,
										   useAsync: false);


			await file.CopyToAsync(fileStream);

			await fileStream.FlushAsync();
			return true;
		} catch(Exception ex) {
			throw ex;
		}
	}

	public Task DeleteAsync(String path, String fileName) {
		File.Delete($"{this.localSettings.GetCombinedPath(path)}/{fileName}");
		return Task.CompletedTask;
	}

	public List<String> GetFiles(String path) {
		DirectoryInfo directory = new(this.localSettings.GetCombinedPath(path));
		return directory.GetFiles().Select(file => file.Name).ToList();
	}

	public Task DeletePath(String path) {
		Directory.Delete(this.localSettings.GetCombinedPath(path), true);
		return Task.CompletedTask;
	}

	public Boolean HasFile(String path, String fileName) {
		return File.Exists($"{this.localSettings.GetCombinedPath(path)}/{fileName}");
	}

	public async Task<List<(String fileName, String path)>> UploadAsync(String path, IFormFileCollection files) {
		this.DirectoryIfExistsCreate(this.localSettings.GetCombinedPath(path));

		List<(String fileName, String path)> uploadedFiles = new();

		foreach(IFormFile file in files) {
			String fileFullName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
			String pathWithFileName = $"{path}/{fileFullName}";
			await CopyFileAsync(this.localSettings.GetCombinedPath(pathWithFileName), file);
			uploadedFiles.Add((fileFullName, pathWithFileName));
		}

		return uploadedFiles;
	}

	private void DirectoryIfExistsCreate(String path) {
		if(Directory.Exists(path) is false)
			Directory.CreateDirectory(path);
	}
}