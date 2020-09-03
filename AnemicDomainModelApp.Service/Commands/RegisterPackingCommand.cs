using AnemicDomainModelApp.Data;
using AnemicDomainModelApp.Domain;

using FluentValidation.Results;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
        private readonly WmsContext _context;

        public RegisterPackingCommandHandler(WmsContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> Handle(RegisterPackingCommand command, CancellationToken cancellationToken)
        {
            var validationFailures = new List<ValidationFailure>();

            var unit = await _context.Units.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == command.CommandDto.UnitId);
            if (unit == null)
                validationFailures.Add(new ValidationFailure("UnitId", "Unit not found"));

            var packingStatus = await _context.PackagingStatus.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == command.CommandDto.PackingStatusId);
            if (packingStatus == null)
                validationFailures.Add(new ValidationFailure("PackingStatusId", "Packing not found"));

            var product = await _context.Products.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == command.CommandDto.ProductId);
            if (product == null)
                validationFailures.Add(new ValidationFailure("ProductId", "Packing not found"));

            var packing = new Packing(command.CommandDto.ConvertionFactor,
                unit.Id, packingStatus.Id, product.Id);
            var validator = new PackingValidator();
            var result = validator.Validate(packing);
            if (!result.IsValid)
                validationFailures.AddRange(result.Errors);

            var validationResult = new ValidationResult(validationFailures);
            if (!validationResult.IsValid)
                return validationResult;

            await _context.Packaging.AddAsync(packing);
            await _context.SaveChangesAsync();
            return validationResult;
        }
    }
}
