using AnemicDomainModelApp.Domain;

using FluentValidation.Results;

using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AnemicDomainModelApp.Service.Commands
{
    public sealed class RegisterPackingCommand : IRequest<ValidationResult>
    {
        public RegisterPackingCommandDto CommandDto { get; }

        public RegisterPackingCommand(RegisterPackingCommandDto commandDto)
        {
            CommandDto = commandDto;
        }

        internal sealed class RegisterPackingCommandHandler : IRequestHandler<RegisterPackingCommand, ValidationResult>
        {
            private readonly WmsContext _context;

            public RegisterPackingCommandHandler(WmsContext context)
            {
                _context = context;
            }

            public async Task<ValidationResult> Handle(RegisterPackingCommand command, CancellationToken cancellationToken)
            {
                List<ValidationFailure> validationFailures = new List<ValidationFailure>();

                var productRepository = new ProductRepository(_context);

                var getProductById = productRepository.GetById(command.CommandDto.ProductId);

                var convertionFactor = ConversionFactor.Create(command.CommandDto.ConvertionFactor);
                if (convertionFactor.IsFailure)
                    validationFailures.Add(new ValidationFailure(nameof(ConversionFactor), convertionFactor.Error));

                var unit = Domain.Unit.FromId(command.CommandDto.UnitId);
                if (unit.IsFailure)
                    validationFailures.Add(new ValidationFailure(nameof(Domain.Unit), unit.Error));

                var packingStatus = PackingStatus.FromId(command.CommandDto.PackingStatusId);
                if (packingStatus.IsFailure)
                    validationFailures.Add(new ValidationFailure(nameof(PackingStatus), packingStatus.Error));

                var product = await getProductById;
                if (product.IsFailure)
                    validationFailures.Add(new ValidationFailure(nameof(Product), product.Error));

                var validationResult = new ValidationResult(validationFailures);
                if (!validationResult.IsValid)
                    return validationResult;

                var packing = new Packing(convertionFactor.Value, unit.Value, packingStatus.Value, product.Value);

                product.Value.AddPacking(packing);

                await _context.SaveChangesAsync();
                return validationResult;
            }
        }
    }
}
