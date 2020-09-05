using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using AnemicDomainModelApp.Domain;
using Microsoft.EntityFrameworkCore;
using CSharpFunctionalExtensions;

namespace AnemicDomainModelApp.Service.Commands
{
    public class RegisterPackingCommand : IRequest<ValidationResult>
    {
        public RegisterPackingCommandDto CommandDto { get; }

        public RegisterPackingCommand(RegisterPackingCommandDto commandDto)
        {
            CommandDto = commandDto;
        }
    }

    public class RegisterPackingCommandHandler : IRequestHandler<RegisterPackingCommand, ValidationResult>
    {
        private readonly ProductRepository _repository;

        public RegisterPackingCommandHandler(ProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(RegisterPackingCommand command, CancellationToken cancellationToken)
        {
            List<ValidationFailure> validationFailures = new List<ValidationFailure>();

            var getProductById = _repository.GetById(command.CommandDto.ProductId);

            List<Result> result = new List<Result>();
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

            return validationResult;
        }
    }
}
