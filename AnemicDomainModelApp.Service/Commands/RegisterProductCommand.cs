using AnemicDomainModelApp.Domain;

using FluentValidation.Results;

using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AnemicDomainModelApp.Service.Commands
{
    public sealed class RegisterProductCommand : IRequest<ValidationResult>
    {
        public RegisterProductCommandDto CommandDto { get; }

        public RegisterProductCommand(RegisterProductCommandDto commandDto)
        {
            CommandDto = commandDto;
        }

        internal sealed class RegisterProductCommandHandler : IRequestHandler<RegisterProductCommand, ValidationResult>
        {
            private readonly WmsContext _context;

            public RegisterProductCommandHandler(WmsContext context)
            {
                _context = context;
            }

            public async Task<ValidationResult> Handle(RegisterProductCommand command, CancellationToken cancellationToken)
            {
                List<ValidationFailure> validationFailures = new List<ValidationFailure>();

                var description = Description.Create(command.CommandDto.Description);
                if (description.IsFailure)
                    validationFailures.Add(new ValidationFailure(nameof(Description), description.Error));

                var netWeight = NetWeight.Create(command.CommandDto.NetWeight);
                if (netWeight.IsFailure)
                    validationFailures.Add(new ValidationFailure(nameof(netWeight), netWeight.Error));

                var productStatus = ProductStatus.FromId(command.CommandDto.ProductStatusId);
                if (productStatus.IsFailure)
                    validationFailures.Add(new ValidationFailure(nameof(productStatus), productStatus.Error));

                var unit = Domain.Unit.FromId(command.CommandDto.UnitId);
                if (unit.IsFailure)
                    validationFailures.Add(new ValidationFailure(nameof(Domain.Unit), unit.Error));

                var validationResult = new ValidationResult(validationFailures);
                if (!validationResult.IsValid)
                    return validationResult;

                var product = new Product(description.Value,
                    netWeight.Value, productStatus.Value, unit.Value);

                var productRepository = new ProductRepository(_context);
                productRepository.Save(product);

                await _context.SaveChangesAsync();
                return validationResult;
            }
        }
    }
}
