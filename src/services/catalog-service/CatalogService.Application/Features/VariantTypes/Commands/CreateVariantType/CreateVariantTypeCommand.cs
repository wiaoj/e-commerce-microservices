using CatalogService.Application.Dtos.Requests.VariantType;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.VariantTypes.Commands.CreateVariantType;
public sealed record CreateVariantTypeCommand : IRequest<Unit> {
	public required CreateVariantTypeRequest CreateVariantTypeRequest { get; init; }

	private class Handler : IRequestHandler<CreateVariantTypeCommand, Unit> {
		private readonly IVariantTypeService variantTypeService;

		public Handler(IVariantTypeService variantTypeService) {
			this.variantTypeService = variantTypeService;
		}

		public async Task<Unit> Handle(CreateVariantTypeCommand request, CancellationToken cancellationToken) {
			await this.variantTypeService.AddVariantAsync(request.CreateVariantTypeRequest, cancellationToken);

			return Unit.Value;
		}
	}
}