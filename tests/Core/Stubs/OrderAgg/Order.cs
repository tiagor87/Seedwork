namespace TRDomainDriven.Core.Tests.Stubs.OrderAgg
{
    public class Order : AggregateRoot<long>
    {
        public Order(long id) : base(id)
        {
        }
    }
}