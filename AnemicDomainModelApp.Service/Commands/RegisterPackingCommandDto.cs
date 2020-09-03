namespace AnemicDomainModelApp.Service.Commands
{
    public class RegisterPackingCommandDto
    {
        public RegisterPackingCommandDto(decimal convertionFactor, int unitId, int packingStatusId, int productId)
        {
            ConvertionFactor = convertionFactor;
            UnitId = unitId;
            PackingStatusId = packingStatusId;
            ProductId = productId;
        }
        public decimal ConvertionFactor { get; }
        public int UnitId { get; }
        public int PackingStatusId { get; }
        public int ProductId { get; }
    }
}
