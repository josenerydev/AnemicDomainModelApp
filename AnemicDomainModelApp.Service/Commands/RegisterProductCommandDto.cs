﻿namespace AnemicDomainModelApp.Service.Commands
{
    public class RegisterProductCommandDto
    {
        public RegisterProductCommandDto(string description, decimal netWeight, int productStatusId, int unitId)
        {
            Description = description;
            NetWeight = netWeight;
            ProductStatusId = productStatusId;
            UnitId = unitId;
        }
        public string Description { get; }
        public decimal NetWeight { get; }
        public int ProductStatusId { get; }
        public int UnitId { get; }
    }
}
