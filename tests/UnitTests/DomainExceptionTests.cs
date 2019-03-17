using System;
using FluentAssertions;
using Seedwork.DomainDriven.Core;
using Xunit;

namespace Seedwork.DomainDriven.UnitTests
{
    public class DomainExceptionTests
    {
        [Theory(DisplayName = "GIVEN domain exception, SHOULD instantiate")]
        [InlineData("Exception message 1")]
        [InlineData("Exception message 2")]
        public void Given_domain_exception_should_instantiate(string message)
        {
            var exception = new DomainException(message);

            exception.Should().NotBeNull();
            exception.Message.Should().Be(message);
        }

        [Fact(DisplayName = "Given domain exception, SHOULD instantiate with inner exception")]
        public void Given_domain_exception_should_instantiate_with_exception()
        {
            var exception = new DomainException("Exception message", new Exception("Inner exception message"));

            exception.Should().NotBeNull();
            exception.Message.Should().Be("Exception message");
            exception.InnerException.Should().NotBeNull();
            exception.InnerException.Message.Should().Be("Inner exception message");
        }
    }
}