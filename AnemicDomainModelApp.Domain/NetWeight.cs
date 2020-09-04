using CSharpFunctionalExtensions;

using System.Collections.Generic;

namespace AnemicDomainModelApp.Domain
{
    public class NetWeight : ValueObject
    {
        public decimal Value { get; }

        private NetWeight(decimal value)
        {
            Value = value;
        }

        public static Result<NetWeight> Create(decimal netWeight)
        {
            if (netWeight <= 0)
                return Result.Failure<NetWeight>("NetWeight could not be zero or nagative");

            return Result.Success(new NetWeight(netWeight));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static explicit operator NetWeight(decimal netWeight)
        {
            return Create(netWeight).Value;
        }

        public static implicit operator decimal(NetWeight netWeight)
        {
            return netWeight.Value;
        }
    }
}