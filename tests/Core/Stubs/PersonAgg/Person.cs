using Seedwork.DomainDriven.Core;
using Seedwork.DomainDriven.UnitTests.Stubs.PersonAgg.Events;
using Seedwork.DomainDriven.UnitTests.Stubs.PersonAgg.ValueObjects;

namespace Seedwork.DomainDriven.UnitTests.Stubs.PersonAgg
{
    public class Person : AggregateRoot<long>
    {
        public Person(long id, Name name) : base(id)
        {
            Name = name;
        }

        public Person(Name name)
        {
            Name = name;
            RaiseDomainEvent(new PersonCreated());
        }

        public Name Name { get; }
    }
}