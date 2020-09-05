using AnemicDomainModelApp.Domain.Common;

using CSharpFunctionalExtensions;

using System.Linq;

namespace AnemicDomainModelApp.Domain
{
    public class ProductStatus : Entity
    {
        public static ProductStatus Ativo = new ProductStatus(1, "Ativo");
        public static ProductStatus Inativo = new ProductStatus(2, "Inativo");
        public static ProductStatus Bloqueado = new ProductStatus(3, "Bloqueado");

        public static ProductStatus[] AllProductStatus = { Ativo, Inativo, Bloqueado };

        public string Name { get; }
        protected ProductStatus()
        {
        }

        private ProductStatus(int id, string name)
            : base(id)
        {
            Name = name;
        }

        public static Result<ProductStatus> FromId(int id)
        {
            var productStatus = AllProductStatus.SingleOrDefault(x => x.Id == id);
            if (productStatus == null)
                return Result.Failure<ProductStatus>("Product Status not found");

            return Result.Success(productStatus);
        }
    }
}
