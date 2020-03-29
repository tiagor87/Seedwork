using Seedwork.DomainDriven.Core;

namespace Seedwork.DomainDriven.UnitTests.Stubs.OrderAgg
{
    public class Order : AggregateRoot<long>
    {
        public Order(long id) : base(id)
        {
        }
    }
}