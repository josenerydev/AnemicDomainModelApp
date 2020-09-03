using System.Collections.Generic;

namespace AnemicDomainModelApp.Domain
{
    public class PackingStatus
    {
        public PackingStatus(string value)
        {
            Value = value;
        }
        public int Id { get; private set; }
        public string Value { get; set; }
        public virtual ICollection<Packing> Packaging { get; private set; } = new List<Packing>();
    }
}
