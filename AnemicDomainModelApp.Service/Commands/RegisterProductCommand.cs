using AnemicDomainModelApp.Domain;

using CSharpFunctionalExtensions;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace AnemicDomainModelApp.Service.Commands
{
    public class RegisterProductCommand : IRequest<Result>
    {
        public RegisterProductCommandDto CommandDto { get; }

        public RegisterProductCommand(RegisterProductCommandDto commandDto)
        {
            CommandDto = commandDto;
        }
    }

    public class RegisterProductCommandHandler : IRequestHandler<RegisterProductCommand, Result>
    {
        private readonly ProductRepository _repository;

        public RegisterProductCommandHandler(ProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(RegisterProductCommand command, CancellationToken cancellationToken)
        {
            var description = Description.Create(command.CommandDto.Description);
            if (description.IsFailure)
                return Result.Failure(description.Error);

            var netWeight = NetWeight.Create(command.CommandDto.NetWeight);
            if (netWeight.IsFailure)
                return Result.Failure(netWeight.Error);

            var productStatus = ProductStatus.FromId(command.CommandDto.ProductStatusId);
            if (productStatus.IsFailure)
                return Result.Failure(productStatus.Error);

            var unit = Domain.Unit.FromId(command.CommandDto.UnitId);
            if (unit.IsFailure)
                return Result.Failure(unit.Error);

            var product = new Product(description.Value,
                netWeight.Value, productStatus.Value, unit.Value);

            _repository.Save(product);

            return Result.Success();
        }
    }
}
