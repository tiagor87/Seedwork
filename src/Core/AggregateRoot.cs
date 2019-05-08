namespace Seedwork.DomainDriven.Core
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot(object id) : base(id)
        {
        }

        protected AggregateRoot()
        {
        }
    }
}