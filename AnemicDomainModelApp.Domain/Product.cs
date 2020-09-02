using FluentValidation;

using System.Collections.Generic;

namespace AnemicDomainModelApp.Domain
{
    public class Product
    {
        protected Product()
        {
        }

        public Product(string description, decimal netWeight, int productStatusId, int unitId)
        {
            Description = description;
            NetWeight = netWeight;
            ProductStatusId = productStatusId;
            UnitId = unitId;
        }

        public int Id { get; private set; }
        public string Description { get; set; }
        public decimal NetWeight { get; set; }
        public int ProductStatusId { get; set; }
        public virtual ProductStatus ProductStatus { get; set; }
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<Packing> Packaging { get; set; } = new List<Packing>();
    }

    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty();
            RuleFor(x => x.NetWeight)
                .NotEmpty();
            RuleFor(x => x.ProductStatusId)
                .NotEmpty();
            RuleFor(x => x.UnitId)
                .NotEmpty();
        }
    }
}
