﻿using BuildingBlocks.Application.Abstraction.Pagination;
using CatalogService.Application.Features.Categories.Dtos;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.Categories.Queries.GetCategories;
public sealed record GetCategoriesQuery : IRequest<IPaginate<GetCategoriesDto>> {
	private class Handler : IRequestHandler<GetCategoriesQuery, IPaginate<GetCategoriesDto>> {
		private readonly ICategoryService categoryService;

		public Handler(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public Task<IPaginate<GetCategoriesDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken) {
			return this.categoryService.GetCategoriesAsync(request, cancellationToken);
		}
	}
}