using System.Collections.Generic;

namespace AnemicDomainModelApp.Domain
{
    public class Unit
    {
        public Unit(string value)
        {
            Value = value;
        }
        public int Id { get; private set; }
        public string Value { get; set; }
        public virtual ICollection<Product> Products { get; private set; } = new List<Product>();
        public virtual ICollection<Packing> Packaging { get; private set; } = new List<Packing>();
    }
}
