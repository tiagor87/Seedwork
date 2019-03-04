namespace Seedwork.DomainDriven.Core
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot(long id) : base(id)
        {
        }

        protected AggregateRoot()
        {
        }
    }
}