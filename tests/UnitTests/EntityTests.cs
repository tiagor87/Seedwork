using System;
using FluentAssertions;
using Seedwork.DomainDriven.UnitTests.Stubs.OrderAgg;
using Seedwork.DomainDriven.UnitTests.Stubs.PersonAgg;
using Seedwork.DomainDriven.UnitTests.Stubs.PersonAgg.ValueObjects;
using Xunit;

namespace Seedwork.DomainDriven.UnitTests
{
    [Trait("Category", "UnitTests")]
    [Trait("Class", "Entity")]
    public class EntityTests
    {
        [Theory(DisplayName = @"GIVEN an Entity, SHOULD instantiate")]
        [InlineData(1, "Name of the first person")]
        [InlineData(2, "Name of the second person")]
        public void Given_entity_should_instantiate(long id, string name)
        {
            var person = new Person(id, new Name(name));

            person.Should().NotBeNull();
            person.Id.Should().Be(id);
            person.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            person.Name.Should().Be(new Name(name));
        }

        [Theory(DisplayName = @"GIVEN two entites, WHEN id is equal, SHOULD be equal")]
        [InlineData(1, "Name", 1, "Name")]
        [InlineData(2, "Name", 2, "Name")]
        public void Given_entities_when_id_equal_should_be_equal(long id1, string name1, long id2, string name2)
        {
            var person1 = new Person(id1, new Name(name1));
            var person2 = new Person(id2, new Name(name2));

            person1.Should().Be(person2);
            (person1 == person2).Should().BeTrue();
            (person1 != person2).Should().BeFalse();
        }

        [Theory(DisplayName = @"GIVEN entity, WHEN id is not null, SHOULD return id hashcode")]
        [InlineData(1, "Name 1")]
        [InlineData(2, "Name 2")]
        public void Given_entity_when_id_not_null_should_hashcode_be_id_hashcode(long id, string name)
        {
            var person = new Person(id, new Name(name));

            person.GetHashCode().Should().Be(id.GetHashCode());
        }

        [Theory(DisplayName = @"GIVEN entity, WHEN transient, SHOULD return base class hashcode")]
        [InlineData("Name 1")]
        [InlineData("Name 2")]
        public void Given_entity_when_transient_should_return_base_class_hashcode(string name)
        {
            var person = new Person(new Name(name));

            person.GetHashCode().Should().NotBe(0);
        }

        [Theory(DisplayName = @"GIVEN entity, WHEN transient and same reference, SHOULD be equal")]
        [InlineData("Name 1")]
        [InlineData("Name 2")]
        public void Given_entity_when_transient_and_different_reference_should_not_be_equal(string name)
        {
            var person1 = new Person(new Name(name));
            var person2 = new Person(new Name(name));

            person1.Should().NotBe(person2);
            (person1 == person2).Should().BeFalse();
            (person1 != person2).Should().BeTrue();
        }

        [Theory(DisplayName = @"GIVEN entities, WHEN transient and same reference, SHOULD be equal")]
        [InlineData("Name 1")]
        [InlineData("Name 2")]
        [InlineData("Name 3")]
        public void Given_entities_when_transient_and_same_reference_should_be_equal(string name)
        {
            var person1 = new Person(new Name(name));
            var person2 = person1;

            person1.Should().Be(person2);
            (person1 == person2).Should().BeTrue();
            (person1 != person2).Should().BeFalse();
        }

        [Theory(DisplayName = @"GIVEN entities, WHEN other is null, SHOULD not be equal")]
        [InlineData("Name 1")]
        public void Given_entities_when_other_null_should_not_be_equal(string name)
        {
            var person1 = new Person(new Name(name));
            Person person2 = null;

            person1.Equals(person2).Should().BeFalse();
            (person1 == person2).Should().BeFalse();
            (person1 != person2).Should().BeTrue();
        }

        [Theory(DisplayName = @"GIVEN entities, WHEN different types, SHOULD not be equal")]
        [InlineData(1, "Name 1", 1)]
        public void Given_entities_when_different_types_should_not_be_equal(long personId, string name, long orderId)
        {
            var person = new Person(personId, new Name(name));
            var order = new Order(orderId);

            person.Should().NotBe(order);
            (person == order).Should().BeFalse();
            (person != order).Should().BeTrue();
        }

        [Fact(DisplayName = @"GIVEN entities, WHEN compare null values, SHOULD be equals")]
        public void Given_entities_when_compare_null_values_should_be_equals()
        {
            Person person = null;

            (person == null).Should().BeTrue();
        }
    }
}