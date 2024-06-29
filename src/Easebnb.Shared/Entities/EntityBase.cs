namespace Easebnb.Shared.Entities;

public abstract class EntityBase<T> : IEntityBase<T>
    where T : IEquatable<T>
{
    public T Id { get; set; }
}
