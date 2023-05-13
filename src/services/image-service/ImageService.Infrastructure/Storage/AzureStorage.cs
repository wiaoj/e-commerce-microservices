using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ImageService.Application.Storage;
using ImageService.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ImageService.Infrastructure.Storage;
public class AzureStorage : IAzureStorage {
	private readonly BlobServiceClient blobServiceClient;

	public AzureStorage(IConfiguration configuration) {
		this.blobServiceClient = new(configuration.GetStorageConnectionString("Azure"));
	}

	private BlobContainerClient BlobContainerClient => this.blobServiceClient.GetBlobContainerClient(path);

	private String path { get; set; } = String.Empty;

	private BlobClient GetBlobClient(String fileName) {
		return this.BlobContainerClient.GetBlobClient(fileName);
	}

	public async Task DeleteAsync(String path, String fileName) {
		this.path = path;
		await this.GetBlobClient(fileName).DeleteAsync();
	}

	public List<String> GetFiles(String path) {
		this.path = path;
		return this.BlobContainerClient.GetBlobs().Select(file => file.Name).ToList();
	}

	public Boolean HasFile(String path, String fileName) {
		this.path = path;
		return this.BlobContainerClient.GetBlobs().Any(file => file.Name.Equals(fileName));
	}

	public async Task<List<(String fileName, String path)>> UploadAsync(String path, IFormFileCollection files) {
		this.path = path;
		await this.BlobContainerClient.CreateIfNotExistsAsync();
		await this.BlobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

		List<(String fileName, String pathOrContainer)> datas = new();
		files.ToList().ForEach(async file => {
			await this.GetBlobClient(file.FileName).UploadAsync(file.OpenReadStream());
			datas.Add((file.FileName, $"{path}/{file.FileName}"));
		});

		return datas;
	}
}