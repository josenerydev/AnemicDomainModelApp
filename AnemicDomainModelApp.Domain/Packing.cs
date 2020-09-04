namespace AnemicDomainModelApp.Domain
{
    public class Packing : Entity
    {
        protected Packing()
        {
        }

        public Packing(ConversionFactor convertionFactor, Unit unit, PackingStatus packingStatus, Product product)
        {
            ConvertionFactor = convertionFactor;
            Unit = unit;
            PackingStatus = packingStatus;
            Product = product;
        }
        public ConversionFactor ConvertionFactor { get; }
        public virtual Unit Unit { get; }
        public virtual PackingStatus PackingStatus { get; }
        public virtual Product Product { get; }
    }
}
