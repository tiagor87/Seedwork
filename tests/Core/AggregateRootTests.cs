using System;
using System.Linq;
using FluentAssertions;
using Seedwork.DomainDriven.UnitTests.Stubs.PersonAgg;
using Seedwork.DomainDriven.UnitTests.Stubs.PersonAgg.Events;
using Seedwork.DomainDriven.UnitTests.Stubs.PersonAgg.ValueObjects;
using Xunit;

namespace Seedwork.DomainDriven.UnitTests
{
    public class AggregateRootTests
    {
        [Fact(DisplayName = @"GIVEN aggregate_root, SHOULD clear domain event")]
        public void Given_aggregate_root_should_clear_domain_event()
        {
            var person = new Person(new Name("Name"));
            person.ClearDomainEvents();

            person.DomainEvents.Should().BeEmpty();
        }

        [Fact(DisplayName = @"GIVEN aggregate_root, SHOULD raise domain event")]
        public void Given_aggregate_root_should_raise_domain_event()
        {
            var person = new Person(new Name("Name"));

            person.DomainEvents.Should().NotBeEmpty();
            person.DomainEvents.OfType<PersonCreated>().Should().NotBeEmpty();
            person.DomainEvents.OfType<PersonCreated>().First().CreatedAt.Should()
                .BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }
    }
}