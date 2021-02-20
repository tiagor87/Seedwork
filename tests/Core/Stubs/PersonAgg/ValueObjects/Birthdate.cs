using System;

namespace TRDomainDriven.Core.Tests.Stubs.PersonAgg.ValueObjects
{
    public class Birthdate : ValueOf<DateTime>
    {
        public Birthdate(DateTime value) : base(value.Date, TryValidate)
        {
        }

        private static bool TryValidate(DateTime birthdate, out Exception exception)
        {
            if (birthdate > DateTime.UtcNow.Date)
            {
                exception = new Exception("Birthdate is invalid.");
                return false;
            }

            exception = null;
            return true;
        }
    }
}