using AnemicDomainModelApp.Data;
using AnemicDomainModelApp.Domain;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace AnemicDomainModelApp.Service.Commands
{
    public class RegisterProductCommand : IRequest<MediatR.Unit>
    {
        public RegisterProductCommandDto CommandDto { get; }

        public RegisterProductCommand(RegisterProductCommandDto commandDto)
        {
            CommandDto = commandDto;
        }
    }

    public class RegisterProductCommandHandler : IRequestHandler<RegisterProductCommand, MediatR.Unit>
    {
        private readonly WmsContext _context;

        public RegisterProductCommandHandler(WmsContext context)
        {
            _context = context;
        }

        public async Task<MediatR.Unit> Handle(RegisterProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product(command.CommandDto.Description,
                command.CommandDto.NetWeight,
                command.CommandDto.ProductStatusId,
                command.CommandDto.UnitId);

            var validator = new ProductValidator();
            var result = validator.Validate(product);
            if (!result.IsValid)
                return await MediatR.Unit.Task;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return await MediatR.Unit.Task;
        }
    }
}
