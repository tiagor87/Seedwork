using TRDomainDriven.Core.Tests.Stubs.PersonAgg.Events;
using TRDomainDriven.Core.Tests.Stubs.PersonAgg.ValueObjects;

namespace TRDomainDriven.Core.Tests.Stubs.PersonAgg
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

        public Person(Name name, Birthdate birthdate)
        {
            Name = name;
            Birthdate = birthdate;
        }

        public Name Name { get; }
        public Birthdate Birthdate { get; }
    }
}