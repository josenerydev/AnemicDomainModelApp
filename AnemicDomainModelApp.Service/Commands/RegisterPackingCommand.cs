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
    public class RegisterPackingCommand : IRequest<Result>
    {
        public RegisterPackingCommandDto CommandDto { get; }

        public RegisterPackingCommand(RegisterPackingCommandDto commandDto)
        {
            CommandDto = commandDto;
        }
    }

    public class RegisterPackingCommandHandler : IRequestHandler<RegisterPackingCommand, Result>
    {
        private readonly ProductRepository _repository;

        public RegisterPackingCommandHandler(ProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(RegisterPackingCommand command, CancellationToken cancellationToken)
        {
            var productTask = _repository.GetById(command.CommandDto.ProductId);

            var convertionFactor = ConversionFactor.Create(command.CommandDto.ConvertionFactor);
            if (convertionFactor.IsFailure)
                return Result.Failure(convertionFactor.Error);

            var unit = Domain.Unit.FromId(command.CommandDto.UnitId);
            if (unit.IsFailure)
                return Result.Failure(unit.Error);

            var packingStatus = PackingStatus.FromId(command.CommandDto.PackingStatusId);
            if (packingStatus.IsFailure)
                return Result.Failure(packingStatus.Error);

            var product = await productTask;
            if (product.IsFailure)
                return Result.Failure(product.Error);

            var packing = new Packing(convertionFactor.Value, unit.Value, packingStatus.Value, product.Value);

            product.Value.AddPacking(packing);

            return Result.Success();
        }
    }
}
