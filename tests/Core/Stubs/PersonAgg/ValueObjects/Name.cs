using System.Collections.Generic;
using Seedwork.DomainDriven.Core;

namespace Seedwork.DomainDriven.UnitTests.Stubs.PersonAgg.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName = null)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}