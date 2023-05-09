﻿using BuildingBlocks.Application.Abstraction.Pagination;
using CatalogService.Application.Features.Products.Dtos;

namespace CatalogService.Application.Services;
public interface IProductService {
	public Task AddProductAsync(CreateProductDto createProductDto, CancellationToken cancellationToken);
	public Task DeleteProductAsync(DeleteProductDto deleteProductDto, CancellationToken cancellationToken);
	public Task UpdateProductAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken);

	public Task<IPaginate<GetProductDto>> GetProductsByCategoryId(Guid categoryId, CancellationToken cancellationToken);
}