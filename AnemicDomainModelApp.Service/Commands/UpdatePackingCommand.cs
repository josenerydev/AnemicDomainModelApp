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
    public class UpdatePackingCommand : IRequest<ValidationResult>
    {
        public UpdatePackingCommandDto CommandDto { get; }
        public UpdatePackingCommand(UpdatePackingCommandDto commandDto)
        {
            CommandDto = commandDto;
        }
    }

    public class UpdatePackingCommandHandler : IRequestHandler<UpdatePackingCommand, ValidationResult>
    {
        private readonly WmsContext _context;

        public UpdatePackingCommandHandler(WmsContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> Handle(UpdatePackingCommand command, CancellationToken cancellationToken)
        {
            var validateFailures = new List<ValidationFailure>();

            var packing = await _context.Packaging.FindAsync(command.CommandDto.Id);
            if (packing == null)
                validateFailures.Add(new ValidationFailure("Id", "Packing not found"));

            var unit = await _context.Units.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == command.CommandDto.UnitId);
            if (unit == null)
                validateFailures.Add(new ValidationFailure("UnitId", "Unit not found"));

            var packingStatus = await _context.ProductsStatus.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == command.CommandDto.PackingStatusId);
            if (packingStatus == null)
                validateFailures.Add(new ValidationFailure("PackingStatusId", "Packing Status not found"));

            var product = await _context.Products.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == command.CommandDto.ProductId);
            if (product == null)
                validateFailures.Add(new ValidationFailure("Product", "Product not found"));

            if (packing.ProductId != product.Id)
                validateFailures.Add(new ValidationFailure(string.Empty, "Invalid operation"));

            packing.ConvertionFactor = command.CommandDto.ConvertionFactor;
            packing.UnitId = unit.Id;
            packing.PackingStatusId = packingStatus.Id;
            packing.ProductId = product.Id;

            var validator = new PackingValidator();
            var result = validator.Validate(packing);
            if (!result.IsValid)
                validateFailures.AddRange(result.Errors);

            var validationResult = new ValidationResult(validateFailures);
            if (!validationResult.IsValid)
                return validationResult;

            await _context.SaveChangesAsync();
            return validationResult;
        }
    }
}
