using Easebnb.Shared.Entities;


namespace Easebnb.Shared;
public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents<TId>(IEnumerable<EntityBase<TId>> entitiesWithEvents) where TId : IEquatable<TId>;
}