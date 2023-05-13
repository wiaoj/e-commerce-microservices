using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ImageService.Application.Storage;
using ImageService.Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ImageService.Infrastructure.Storage;
public class AzureStorage : IAzureStorage {
	private readonly BlobServiceClient blobServiceClient;
	private readonly AzureOptions azureOptions;

	public AzureStorage(IOptions<AzureOptions> azureSettings) {
		this.azureOptions = azureSettings.Value;
		this.blobServiceClient = new(this.azureOptions.ConnectionString);
	}

	private BlobContainerClient BlobContainerClient 
		=> this.blobServiceClient.GetBlobContainerClient(this.azureOptions.Options.ContainerName);

	private BlobClient GetBlobClient(String fileName) {
		return this.BlobContainerClient.GetBlobClient(fileName);
	}

	public async Task DeleteAsync(String path, String fileName) {
		await this.GetBlobClient(fileName).DeleteAsync();
	}

	public List<String> GetFiles(String path) {
		return this.BlobContainerClient.GetBlobs().Select(file => file.Name).ToList();
	}

	public Boolean HasFile(String path, String fileName) {
		return this.BlobContainerClient.GetBlobs().Any(file => file.Name.Equals(fileName));
	}

	public async Task<List<(String fileName, String path)>> UploadAsync(String path, IFormFileCollection files) {
		await this.BlobContainerClient.CreateIfNotExistsAsync();
		await this.BlobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

		List<(String fileName, String pathOrContainer)> datas = new();

		foreach(IFormFile file in files) {
			await this.GetBlobClient(file.FileName).UploadAsync(file.OpenReadStream());
			datas.Add((file.FileName, $"{path}/{file.FileName}"));
		}

		return datas;
	}
}