namespace Easebnb.Shared.Entities;

public abstract class EntityBase<T> : IEntityBase<T>
    where T : IEquatable<T>
{
    public T Id { get; set; }
}

public abstract class EntityAggregateBase<T> : HasDomainEventBase, IEntityBase<T>, IAggregateRoot
    where T : IEquatable<T>
{
    public T Id { get; set; }
}