using CSharpFunctionalExtensions;

using System.Linq;

namespace AnemicDomainModelApp.Domain
{
    public class PackingStatus : Entity
    {
        public static PackingStatus Ativo => new PackingStatus(1, "Ativo");
        public static PackingStatus Inativo => new PackingStatus(2, "Inativo");

        public static PackingStatus[] AllPackingStatus = { Ativo, Inativo };

        public string Name { get; }

        protected PackingStatus()
        {
        }

        private PackingStatus(int id, string name)
            : base(id)
        {
            Name = name;
        }

        public static Result<PackingStatus> FromId(int id)
        {
            var packingStatus = AllPackingStatus.SingleOrDefault(x => x.Id == id);
            if (packingStatus == null)
                return Result.Failure<PackingStatus>("Packing Status not found");

            return Result.Success(packingStatus);
        }
    }
}
