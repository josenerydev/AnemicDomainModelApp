namespace AnemicDomainModelApp.Service.Commands
{
    public class UpdatePackingCommandDto
    {
        public UpdatePackingCommandDto(int id, decimal convertionFactor, int unitId, int packingStatusId, int productId)
        {
            Id = id;
            ConvertionFactor = convertionFactor;
            UnitId = unitId;
            PackingStatusId = packingStatusId;
            ProductId = productId;
        }

        public int Id { get; }
        public decimal ConvertionFactor { get; }
        public int UnitId { get; }
        public int PackingStatusId { get; }
        public int ProductId { get; }
    }
}
