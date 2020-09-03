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
    public class UpdateProductCommand : IRequest<ValidationResult>
    {
        public UpdateProductCommandDto CommandDto { get; }

        public UpdateProductCommand(UpdateProductCommandDto commandDto)
        {
            CommandDto = commandDto;
        }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ValidationResult>
    {
        private readonly WmsContext _context;

        public UpdateProductCommandHandler(WmsContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var validationFailures = new List<ValidationFailure>();

            var product = await _context.Products.FindAsync(command.CommandDto.Id);
            if (product == null)
                validationFailures.Add(new ValidationFailure("Id", "Product not found"));

            var productStatus = await _context.ProductsStatus.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == command.CommandDto.ProductStatusId);
            if (productStatus == null)
                validationFailures.Add(new ValidationFailure("ProductStatusId", "Product Status not found"));

            var unit = await _context.Units.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == command.CommandDto.UnitId);
            if (unit == null)
                validationFailures.Add(new ValidationFailure("UnitId", "Unit not found"));

            product.Description = command.CommandDto.Description;
            product.NetWeight = command.CommandDto.NetWeight;
            product.ProductStatusId = command.CommandDto.ProductStatusId;
            product.UnitId = command.CommandDto.UnitId;

            var validator = new ProductValidator();
            var result = validator.Validate(product);
            if (!result.IsValid)
                validationFailures.AddRange(result.Errors);

            var validationResult = new ValidationResult(validationFailures);
            if (!validationResult.IsValid)
                return validationResult;

            await _context.SaveChangesAsync();
            return validationResult;
        }
    }
}
