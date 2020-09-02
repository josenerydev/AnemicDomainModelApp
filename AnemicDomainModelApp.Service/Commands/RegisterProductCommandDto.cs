namespace AnemicDomainModelApp.Service.Commands
{
    public class RegisterProductCommandDto
    {
        public string Description { get; set; }
        public decimal NetWeight { get; set; }
        public int ProductStatusId { get; set; }
        public int UnitId { get; set; }
    }
}
