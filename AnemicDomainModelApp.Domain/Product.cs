using System.Collections.Generic;
using System.Linq;

namespace AnemicDomainModelApp.Domain
{
    public class Product : Entity
    {
        protected Product()
        {
        }

        public Product(Description description, NetWeight netWeight, ProductStatus productStatus, Unit unit)
        {
            Description = description;
            NetWeight = netWeight;
            ProductStatus = productStatus;
            Unit = unit;
        }

        public Description Description { get; set; }
        public NetWeight NetWeight { get; set; }
        public virtual ProductStatus ProductStatus { get; set; }
        public virtual Unit Unit { get; set; }

        private readonly List<Packing> _packaging = new List<Packing>();
        public virtual IReadOnlyCollection<Packing> Packaging => _packaging.ToList();

        public void AddPacking(Packing packing)
        {
            _packaging.Add(packing);
        }
    }
}
