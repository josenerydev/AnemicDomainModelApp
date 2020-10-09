using AnemicDomainModelApp.Domain;

using FluentValidation.Results;

using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AnemicDomainModelApp.Service.Commands
{
    public class DeleteProductCommand : IRequest<ValidationResult>
    {
        public int Id { get; }

        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ValidationResult>
    {
        private readonly WmsContext _context;

        public DeleteProductCommandHandler(WmsContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var validationFailures = new List<ValidationFailure>();

            var productRepository = new ProductRepository(_context);
            var product = (await productRepository.GetById(command.Id)).Value;
            if (product == null)
                validationFailures.Add(new ValidationFailure("Id", "Product not found"));

            var validationResult = new ValidationResult(validationFailures);
            if (!validationResult.IsValid)
                return validationResult;

            productRepository.Remove(product);

            await _context.SaveChangesAsync();
            return validationResult;
        }
    }
}
