using FluentAssertions;
using TRDomainDriven.Tests.Stubs.PersonAgg.ValueObjects;
using Xunit;

namespace TRDomainDriven.Tests
{
    [Trait("Category", "UnitTests")]
    [Trait("Class", "ValueObject")]
    public class ValueObjectTests
    {
        [Theory(DisplayName = @"GIVEN value object, SHOULD instantiate")]
        [InlineData("First name 1", "Last name 1")]
        [InlineData("First name 2", "Last name 2")]
        [InlineData("First name 3", "Last name 3")]
        public void Given_value_object_should_instantiate(string firtName, string lastName)
        {
            var name = new Name(firtName, lastName);

            name.Should().NotBeNull();
            name.FirstName.Should().Be(firtName);
            name.LastName.Should().Be(lastName);
        }

        [Theory(DisplayName = @"GIVEN value objects, WHEN same values, SHOULD be equal")]
        [InlineData("Name 1", "Name 1")]
        [InlineData("Name 2", "Name 2")]
        public void Given_value_objects_when_same_values_should_be_equal(string value1, string value2)
        {
            var name1 = new Name(value1);
            var name2 = new Name(value2);

            name1.Should().Be(name2);
            (name1 == name2).Should().BeTrue();
            (name1 != name2).Should().BeFalse();
        }

        [Theory(DisplayName = @"GIVEN value objects, WHEN different values, SHOULD not be equal")]
        [InlineData("Name 1", "Name 2")]
        [InlineData("Name 2", "Name 3")]
        public void Given_value_objects_when_different_values_should_not_be_equal(string value1, string value2)
        {
            var name1 = new Name(value1);
            var name2 = new Name(value2);

            name1.Should().NotBe(name2);
            (name1 == name2).Should().BeFalse();
            (name1 != name2).Should().BeTrue();
        }

        [Theory(DisplayName = @"GIVEN value objects, WHEN some value is null, SHOULD not be null")]
        [InlineData("Name 1", null)]
        [InlineData(null, "Name 2")]
        public void Given_value_objects_when_some_value_null_should_not_be_equal(string value1, string value2)
        {
            var name1 = new Name(value1);
            var name2 = new Name(value2);

            name1.Should().NotBe(name2);
        }

        [Theory(DisplayName = @"GIVEN value object, SHOULD return values hashcodes")]
        [InlineData("First name", "Last name")]
        public void Given_value_object_should_return_values_hashcodes(string firstName, string lastName)
        {
            var name = new Name(firstName, lastName);

            name.GetHashCode().Should().Be(firstName.GetHashCode() ^ lastName.GetHashCode());
        }

        [Theory(DisplayName = @"GIVEN value object, WHEN value is null SHOULD return zero")]
        [InlineData(null)]
        public void Given_value_object_when_value_null_should_return_zero(string value)
        {
            var name = new Name(value);

            name.GetHashCode().Should().Be(0);
        }

        [Fact(DisplayName = @"GIVEN value_object, WHEN other null, SHOULD be different")]
        public void Given_value_object_when_other_null_should_be_different()
        {
            var name = new Name("Name");
            name.Equals((object)null).Should().BeFalse();
        }

        [Fact(DisplayName = @"GIVEN value_object, WHEN same reference, SHOULD be equals")]
        public void Given_value_object_when_same_reference_as_object_should_be_equal()
        {
            var name = new Name("Name");
            name.Equals((object)name).Should().BeTrue();
        }

        [Fact(DisplayName = @"GIVEN value_object, WHEN same reference, SHOULD be equals")]
        public void Given_value_object_when_same_reference_should_be_equal()
        {
            var name = new Name("Name");
            name.Equals(name).Should().BeTrue();
        }

        [Fact(DisplayName = @"GIVEN value objects, WHEN compare null values, SHOULD be equals")]
        public void Given_value_objects_when_compare_null_values_should_be_equals()
        {
            Name name = null;

            (name == null).Should().BeTrue();
        }

        [Fact(DisplayName = @"GIVEN value objects, WHEN some is null, SHOULD not be null")]
        public void Given_value_objects_when_some_null_should_not_be_equal()
        {
            var name1 = new Name("Name 1");
            Name name2 = null;

            name1.Equals(name2).Should().BeFalse();
            (name1 == name2).Should().BeFalse();
            (name1 != name2).Should().BeTrue();
        }
    }
}