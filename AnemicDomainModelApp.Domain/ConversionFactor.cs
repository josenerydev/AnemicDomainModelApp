using CSharpFunctionalExtensions;

using System.Collections.Generic;

namespace AnemicDomainModelApp.Domain
{
    public class ConversionFactor : ValueObject
    {
        public decimal Value { get; }

        private ConversionFactor(decimal value)
        {
            Value = value;
        }

        public static Result<ConversionFactor> Create(decimal conversionFactor)
        {
            if (conversionFactor <= 0)
                return Result.Failure<ConversionFactor>("Conversion Factor should not be zero or nagative");

            return Result.Success(new ConversionFactor(conversionFactor));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static explicit operator ConversionFactor(decimal conversionFactor)
        {
            return Create(conversionFactor).Value;
        }

        public static implicit operator decimal(ConversionFactor conversionFactor)
        {
            return conversionFactor.Value;
        }
    }
}