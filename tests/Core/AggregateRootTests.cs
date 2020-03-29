using System;
using System.Linq;
using FluentAssertions;
using TRDomainDriven.Tests.Stubs.PersonAgg;
using TRDomainDriven.Tests.Stubs.PersonAgg.Events;
using TRDomainDriven.Tests.Stubs.PersonAgg.ValueObjects;
using Xunit;

namespace TRDomainDriven.Tests
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