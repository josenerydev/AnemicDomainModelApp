using FluentValidation;

namespace AnemicDomainModelApp.Domain
{
    public class Packing
    {
        protected Packing()
        {
        }

        public Packing(decimal convertionFactor, int unitId, int packingStatusId, int productId)
        {
            ConvertionFactor = convertionFactor;
            UnitId = unitId;
            PackingStatusId = packingStatusId;
            ProductId = productId;
        }

        public int Id { get; private set; }
        public decimal ConvertionFactor { get; set; }
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public int PackingStatusId { get; set; }
        public virtual PackingStatus PackingStatus { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }

    public class PackingValidator : AbstractValidator<Packing>
    {
        public PackingValidator()
        {
            RuleFor(x => x.ConvertionFactor)
                .NotEmpty();
            RuleFor(x => x.UnitId)
                .NotEmpty();
            RuleFor(x => x.PackingStatusId)
                .NotEmpty();
            RuleFor(x => x.ProductId)
                .NotEmpty();
        }
    }
}
