using Microsoft.AspNetCore.Http;

namespace ImageService.Application.Dtos.Requests;
public sealed record UploadImageRequest(Guid ProductId, IFormFileCollection Images);