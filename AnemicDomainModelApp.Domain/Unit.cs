using CSharpFunctionalExtensions;

using System.Linq;

namespace AnemicDomainModelApp.Domain
{
    public class Unit : Entity
    {
        public static readonly Unit Caixa = new Unit(1, "CX - Caixa");
        public static readonly Unit Quilograma = new Unit(2, "KG - Quilograma");
        public static readonly Unit Litro = new Unit(3, "L - Litro");
        public static readonly Unit Peca = new Unit(4, "PC - Peça");
        public static readonly Unit Pacote = new Unit(5, "PCT - Pacote");
        public static readonly Unit Unidade = new Unit(6, "UN - Unidade");

        public static readonly Unit[] AllUnits = { Caixa, Quilograma, Litro, Peca, Pacote, Unidade };

        public string Name { get; }

        protected Unit()
        {
        }

        private Unit(int id, string name)
            : base(id)
        {
            Name = name;
        }

        public static Result<Unit> FromId(int id)
        {
            var unit = AllUnits.SingleOrDefault(x => x.Id == id);
            if (unit == null)
                return Result.Failure<Unit>("Unit not found");

            return Result.Success(unit);
        }
    }
}
