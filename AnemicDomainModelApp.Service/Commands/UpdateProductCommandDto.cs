namespace AnemicDomainModelApp.Service.Commands
{
    public class UpdateProductCommandDto
    {
        public UpdateProductCommandDto(int id, string description, decimal netWeight, int productStatusId, int unitId)
        {
            Id = id;
            Description = description;
            NetWeight = netWeight;
            ProductStatusId = productStatusId;
            UnitId = unitId;
        }
        public int Id { get; }
        public string Description { get; }
        public decimal NetWeight { get; }
        public int ProductStatusId { get; }
        public int UnitId { get; }
    }
}
