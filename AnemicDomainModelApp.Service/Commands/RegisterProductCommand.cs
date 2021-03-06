﻿using AnemicDomainModelApp.Data;
using AnemicDomainModelApp.Domain;

using FluentValidation.Results;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AnemicDomainModelApp.Service.Commands
{
    public class RegisterProductCommand : IRequest<ValidationResult>
    {
        public RegisterProductCommandDto CommandDto { get; }

        public RegisterProductCommand(RegisterProductCommandDto commandDto)
        {
            CommandDto = commandDto;
        }
    }

    public class RegisterProductCommandHandler : IRequestHandler<RegisterProductCommand, ValidationResult>
    {
        private readonly WmsContext _context;

        public RegisterProductCommandHandler(WmsContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> Handle(RegisterProductCommand command, CancellationToken cancellationToken)
        {
            var validationFailures = new List<ValidationFailure>();

            var productStatus = await _context.ProductsStatus
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == command.CommandDto.ProductStatusId);
            if (productStatus == null)
                validationFailures.Add(new ValidationFailure("ProductStatusId", "Product Status not found"));

            var unit = await _context.Units
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == command.CommandDto.UnitId);
            if (unit == null)
                validationFailures.Add(new ValidationFailure("UnitId", "Unit not found"));

            var product = new Product(command.CommandDto.Description,
                command.CommandDto.NetWeight,
                command.CommandDto.ProductStatusId,
                command.CommandDto.UnitId);

            var validator = new ProductValidator();
            var result = validator.Validate(product);
            if (!result.IsValid)
                validationFailures.AddRange(result.Errors);


            var validationResult = new ValidationResult(validationFailures);
            if (!validationResult.IsValid)
                return validationResult;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return validationResult;
        }
    }
}
