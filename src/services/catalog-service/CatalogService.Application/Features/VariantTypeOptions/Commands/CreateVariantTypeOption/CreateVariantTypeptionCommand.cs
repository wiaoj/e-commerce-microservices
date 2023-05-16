using CatalogService.Application.Dtos.Requests.VariantTypeOption;
using CatalogService.Application.Services;
using MediatR;

namespace CatalogService.Application.Features.VariantTypeOptions.Commands.CreateVariantTypeOption;
public sealed record CreateVariantTypeptionCommand : IRequest<Unit> {
	public required CreateVariantTypeOptionRequest CreateVariantTypeOptionRequest { get; init; }

	private class Handler : IRequestHandler<CreateVariantTypeptionCommand, Unit> {
		private readonly IVariantTypeOptionService variantTypeOptionService;

		public Handler(IVariantTypeOptionService variantTypeOptionService) {
			this.variantTypeOptionService = variantTypeOptionService;
		}

		public async Task<Unit> Handle(CreateVariantTypeptionCommand request, CancellationToken cancellationToken) {
			await this.variantTypeOptionService.AddVariantTypeOption(request.CreateVariantTypeOptionRequest, cancellationToken);

			return Unit.Value;
		}
	}
}