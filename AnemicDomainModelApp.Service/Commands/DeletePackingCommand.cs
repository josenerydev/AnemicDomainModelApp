//using AnemicDomainModelApp.Data;

//using FluentValidation.Results;

//using MediatR;

//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;

//namespace AnemicDomainModelApp.Service.Commands
//{
//    public class DeletePackingCommand : IRequest<ValidationResult>
//    {
//        public int Id { get; }

//        public DeletePackingCommand(int id)
//        {
//            Id = id;
//        }
//    }

//    public class DeletePackingCommandHandler : IRequestHandler<DeletePackingCommand, ValidationResult>
//    {
//        private readonly WmsContext _context;
//        public DeletePackingCommandHandler(WmsContext context)
//        {
//            _context = context;
//        }

//        public async Task<ValidationResult> Handle(DeletePackingCommand command, CancellationToken cancellationToken)
//        {
//            var validationFailures = new List<ValidationFailure>();

//            var packing = await _context.Packaging.FindAsync(command.Id);
//            if (packing == null)
//                validationFailures.Add(new ValidationFailure("Id", "Packing not found"));

//            var validationResult = new ValidationResult(validationFailures);
//            if (!validationResult.IsValid)
//                return validationResult;

//            _context.Remove(packing);
//            await _context.SaveChangesAsync();
//            return validationResult;
//        }
//    }
//}
