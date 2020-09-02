using System.Collections.Generic;

namespace AnemicDomainModelApp.Domain
{
    public class PackingStatus
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public virtual ICollection<Packing> Packaging { get; set; } = new List<Packing>();
    }
}
