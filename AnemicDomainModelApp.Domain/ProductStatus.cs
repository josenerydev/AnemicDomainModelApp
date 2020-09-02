using System.Collections.Generic;

namespace AnemicDomainModelApp.Domain
{
    public class ProductStatus
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public virtual ICollection<Product> Products { get; private set; } = new List<Product>();
    }
}
