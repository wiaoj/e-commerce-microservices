using ImageService.Application.Storage;
using Microsoft.AspNetCore.Http;

namespace ImageService.Infrastructure.Storage;
public sealed class LocalStorage : ILocalStorage {
	public async Task<Boolean> CopyFileAsync(String path, IFormFile file) {
		try {
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
		File.Delete($"{path}\\{fileName}");
		return Task.CompletedTask;
	}

	public List<String> GetFiles(String path) {
		DirectoryInfo directory = new(path);
		return directory.GetFiles().Select(file => file.Name).ToList();
	}

	public Boolean HasFile(String path, String fileName) {
		return File.Exists($"{path}\\{fileName}");
	}

	public async Task<List<(String fileName, String path)>> UploadAsync(String path, IFormFileCollection files) {
		string uploadPath = Path.Combine("wwwroot", path);
		if(Directory.Exists(uploadPath) is false)
			Directory.CreateDirectory(uploadPath);

		List<(string fileName, string path)> datas = new();

		foreach(IFormFile file in files) {
			await CopyFileAsync($"{uploadPath}//{file.FileName}", file);
			datas.Add((file.FileName, $"{path}/{file.FileName}"));
		}

		return datas;
	}
}