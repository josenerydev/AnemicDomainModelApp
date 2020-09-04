using CSharpFunctionalExtensions;

using System.Collections.Generic;

namespace AnemicDomainModelApp.Domain
{
    public class Description : ValueObject
    {
        public string Value { get; }

        private Description(string value)
        {
            Value = value;
        }

        public static Result<Description> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<Description>("Description should not be empty");

            description = description.Trim();

            return Result.Success(new Description(description));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static explicit operator Description(string description)
        {
            return Create(description).Value;
        }

        public static implicit operator string(Description description)
        {
            return description.Value;
        }
    }
}