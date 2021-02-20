using System;
using FluentAssertions;
using TRDomainDriven.Core.Tests.Stubs.PersonAgg.ValueObjects;
using Xunit;

namespace TRDomainDriven.Core.Tests
{
    [Trait("Category", "UnitTests")]
    [Trait("Class", "ValueOf")]
    public class ValueOfTests
    {
        [Fact(DisplayName = @"GIVEN value object, SHOULD instantiate")]
        public void Given_value_object_should_instantiate()
        {
            var birthdate = new Birthdate(DateTime.UtcNow);

            birthdate.Should().NotBeNull();
            birthdate.Value.Should().Be(DateTime.UtcNow.Date);
        }
        
        [Fact(DisplayName = @"GIVEN value object, WHEN value invalid SHOULD throw")]
        public void Given_value_object_when_value_invalid_should_throw()
        {
            Func<Birthdate> action = () => new Birthdate(DateTime.UtcNow.AddDays(10));

            action.Should().Throw<Exception>();
        }
        
        [Fact(DisplayName = @"GIVEN value object, WHEN try set primitive value SHOULD implicit convert")]
        public void Given_value_object_when_set_primitive_value_should_implicit_convert()
        {
            DateTime birthdate = new Birthdate(DateTime.UtcNow);

            birthdate.Should().Be(DateTime.UtcNow.Date);
        }
        
        [Fact(DisplayName = @"GIVEN value object, WHEN try set primitive value, AND ValueOf is null SHOULD throw")]
        public void Given_value_object_when_set_primitive_value_and_valueof_is_null_should_throw()
        {
            Birthdate birthdate = null;
            
            Func<DateTime> action = () => birthdate;

            action.Should().Throw<ArgumentNullException>();
        }
        
        [Fact(DisplayName = @"GIVEN value objects, WHEN same values, SHOULD be equal")]
        public void Given_value_objects_when_same_values_should_be_equal()
        {
            var birthdate1 = new Birthdate(DateTime.UtcNow);
            var birthdate2 = new Birthdate(DateTime.UtcNow);

            birthdate1.Should().Be(birthdate2);
            (birthdate1 == birthdate2).Should().BeTrue();
            (birthdate1 != birthdate2).Should().BeFalse();
        }
    }
}