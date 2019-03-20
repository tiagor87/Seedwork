using System;
using System.Diagnostics.CodeAnalysis;

namespace Seedwork.DomainDriven.Core
{
    [Serializable]
    public class DomainException : Exception
    {
        [ExcludeFromCodeCoverage]
        private DomainException()
        {
        }

        public DomainException(string message, Exception exception) : base(message, exception)
        {
        }

        public DomainException(string message) : base(message)
        {
        }
    }
}